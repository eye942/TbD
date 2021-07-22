using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine.Analytics;

public class Fireball : MonoBehaviour
{
    private GameObject spawner;
    private int spawnerID;
    public Color color = Color.green;


    private AudioSource[] clickSounds;
    private int soundIndex;

    private Vector2 initialPosition;
    private Rigidbody2D rigidBody;
    // spring constant
    private float k;

    private float forceX = 0f;
    private int health;
    private int click;

    private int damage;

    public float _elapsedTime = 0;

    private void Start()
    {
        spawner = GameObject.FindWithTag("fireball_spawn");

        rigidBody = GetComponent<Rigidbody2D>();
        clickSounds = GetComponents<AudioSource>();
        soundIndex=0;
       
        // TODO revise spring constant based on difficulty
        k = Random.Range(2, 5);

        var vX = Random.Range(3, 6);
        // v = sqrt(2 / m * (-1/2) k x^2)
        var vY = -Mathf.Sqrt(1 / rigidBody.mass * k * 2 * 3);
        rigidBody.velocity = new Vector2(vX, vY / 1.5f);
        initialPosition = transform.position;
        rigidBody.AddForce(new Vector2(forceX, 0), ForceMode2D.Force);

        // TODO revise max and min based on difficulty
        //health = Random.Range(1, 3);
        health = BalanceManager.FireballMaxHealthPoint;
        click = BalanceManager.FireballMaxClicks;

        // TODO revise max and min based on difficulty
        damage = Random.Range(BalanceManager.FireballMinDamage, BalanceManager.FireballMaxDamage+(int) ResourceManager._elapsedTime/30);
    }

    private void Update()
    {
        transform.Rotate(0, 0, -300 * Time.deltaTime);
        var dy = rigidBody.position.y - initialPosition.y;
        _elapsedTime += Time.deltaTime;

        // Modify k -- spring constant
        // k *= Random.Range(1.0f, 1.3f);
        // k = k <= 0.01 ? k : 0.01f;
        // f_k = -k * dX
        var forceY = -k * dy;
        forceX *= Random.Range(0.001f, 0.005f);
        // Debug.Log($"{dy},{forceY}");

        rigidBody.AddForce(new Vector2(forceX, forceY), ForceMode2D.Force);

        if(transform.position.x >= 15)
        {
            Die();
        }

    }

    private void Reset()
    {
        health = BalanceManager.FireballMaxHealthPoint;
    }

    // Click on fireball to decrease health
    private void OnMouseDown()
    {
        // Debug.Log("Click event");
        click -= 1;

        if (click > 0)
        {

            if (SettingsManager.AudioStateOn) AudioSource.PlayClipAtPoint(clickSounds[soundIndex%6].clip, transform.position);
            soundIndex+=1;
            float new_R = gameObject.GetComponent<Renderer>().material.color.r - (0.75f / BalanceManager.FireballMaxClicks);
            gameObject.GetComponent<Renderer>().material.color = new Color(new_R, 15/255, 15/255, 1);
            health = (int)((float)click / BalanceManager.FireballMaxClicks * BalanceManager.FireballMaxHealthPoint);
            // Debug.Log($"Fireball health is {health}");
        }

        if (click <= 0)
        {
            health = 0;
            // Debug.Log($"Fireball health is {health}");
            GiveManaReward();
            Die();
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Fireball collision");

        if (other.gameObject.CompareTag("tower"))
        {
            other.gameObject.SendMessage("DamageTower", damage);
            GameObject e = Instantiate(explosion) as GameObject;
            e.transform.position = transform.position;
            //yield WaitForSeconds(delay);
            Destroy(e, 1.0f);
            Die();
        }
    }

    private void Die()
    {
        spawner.BroadcastMessage("killEnemy", spawnerID);
        // Debug.Log("Fireball - Die()");
        //this.gameObject.SetActive(false);
        ReportEnemyDeath();

        if (SettingsManager.AudioStateOn) AudioSource.PlayClipAtPoint(clickSounds[4].clip, transform.position);

        if (SettingsManager.AudioStateOn) AudioSource.PlayClipAtPoint(clickSounds[5].clip, transform.position);


        Destroy(gameObject);
       
        
    }


    private void GiveManaReward()
    {
        GameObject resourceManagerObject = GameObject.Find("ResourceManager");
        ResourceManager resourceManager = resourceManagerObject.GetComponent<ResourceManager>();
        resourceManager.IncreaseMana(BalanceManager.ManaFireballReward);
    }

    // add explosion effect
    public GameObject explosion;

    // this gets called in the beginning when it is created by the spawner script
    void setName(int sName)
    {
        spawnerID = sName;
    }

    public void ReportEnemyDeath()
    {
        int timeOfDeath = (int)_elapsedTime % 60;
        Analytics.CustomEvent("EnemyDiedPosition", new Dictionary<string, object>
        {
            { "FireballDiedPosition", this.gameObject.transform.position.x.ToString("F")},
            { "FireballTimeOfDeath", timeOfDeath }
        });
        Debug.Log("Fireball Position of Death: " + this.gameObject.transform.position.x.ToString("F"));
        Debug.Log("Fireball Time of Death: " + timeOfDeath);
    }

}
