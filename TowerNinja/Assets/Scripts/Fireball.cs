using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fireball : MonoBehaviour
{
    private GameObject spawner;
    private int spawnerID;

    private Vector2 initialPosition;
    private Rigidbody2D rigidBody;
    // spring constant
    private float k;

    private float forceX = 0f;
    private int health;

    private int damage;

    public int FireballManaReward;

    private void Start()
    {
        spawner = GameObject.FindWithTag("fireball_spawn");
        
        rigidBody = GetComponent<Rigidbody2D>();
        // TODO revise spring constant based on difficulty
        k = Random.Range(2, 5);

        var vX = Random.Range(3, 6);
        // v = sqrt(2 / m * (-1/2) k x^2)
        var vY = -Mathf.Sqrt(1 / rigidBody.mass * k * 3 * 3);
        rigidBody.velocity = new Vector2(vX, vY/1.5f);
        initialPosition = transform.position;
        rigidBody.AddForce(new Vector2(forceX, 0), ForceMode2D.Force);

        // TODO revise max and min based on difficulty
        health = Random.Range(1, 3);

        // TODO revise max and min based on difficulty
        damage = Random.Range(1, 3);
    }

    private void Update()
    {
        var dy = rigidBody.position.y - initialPosition.y;

        // Modify k -- spring constant
        // k *= Random.Range(1.0f, 1.3f);
        // k = k <= 0.01 ? k : 0.01f;
        // f_k = -k * dX
        var forceY = -k * dy;
        forceX *= Random.Range(0.001f, 0.005f);
        // Debug.Log($"{dy},{forceY}");

        rigidBody.AddForce(new Vector2(forceX, forceY), ForceMode2D.Force);
    }

    // Click on fireball to decrease health
    private void OnMouseDown()
    {
        Debug.Log("Click event");
        health -= 1;
        if (health <= 0)
        {
            GiveManaReward();
            Die();
        }
    }

    private void OnMouseDrag()
    {
        Debug.Log("Drag event");
        health -= 1;
        if (health <= 0)
        {
            GiveManaReward();
            Die();
        }
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse Over");

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Fireball collision");
        
        if (other.gameObject.CompareTag("friendly"))
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
        Debug.Log("Fireball - Die()");
        //this.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void GiveManaReward()
    {
        GameObject resourceManagerObject = GameObject.Find("ResourceManager").gameObject;
        ResourceManager resourceManager = resourceManagerObject.GetComponent<ResourceManager>();
        resourceManager.IncreaseMana(FireballManaReward);
    }

    // add explosion effect
    public GameObject explosion;

    // this gets called in the beginning when it is created by the spawner script
    void setName(int sName)
    {
        spawnerID = sName;
    }
}
