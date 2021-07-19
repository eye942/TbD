using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;


// Instantiate a rigidbody then set the velocity

public class enemyDmgHitbox : MonoBehaviour
{
    private Rigidbody2D thisRB;
    public int MaxHealthPoint = 50;
    private static readonly int MinHealthPoint = 0;
    private int _healthPoint;
    //public GameObject parentOfHitbox;
    public float damageTime;
    public float damageTimer;
    public float _elapsedTime;
    public int damage;
    //checks if the enemy is doing damage
    private bool damageBool = false;
    private Vector2 velocity;
    private int totalCollisions;
    public float Velocity;
    // health text
    public TMPro.TextMeshPro _healthText;
    GameObject textobj;

    private float origY;
    bool animationStarted = false;
    void Start()
    {
        textobj = this.gameObject.transform.GetChild (0).gameObject;
        _healthText = textobj.GetComponent<TextMeshPro>();
        _healthText.text = MaxHealthPoint + "/" + MaxHealthPoint;
        // textmeshpro and textmeshprougui
    	
        /*_healthText = AddComponent<TMPro.TextMeshPro>();
    	if (_healthText == null) {
    		Debug.Log("ehealth - IS NULL");
    	}
        */
        _elapsedTime += Time.deltaTime;
        totalCollisions = 0;
        if (EnemyWave.numWaves > 5)
        {
            _healthPoint = Mathf.RoundToInt(MaxHealthPoint * (EnemyWave.numWaves - 5) * 1.2f);
            damage = Mathf.RoundToInt(damage * (EnemyWave.numWaves - 5) * 1.2f);
        }
        else
        {
            _healthPoint = MaxHealthPoint;
        }
        //damageTime = 1.5f;
        damageTimer = 0.0f;
        thisRB = this.GetComponent<Rigidbody2D>();
        thisRB.velocity = new Vector2(Velocity, 0);
        EnemyWave.spawnedEnemy++;
        origY = this.transform.position.y;
    }
    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (damageBool)
        {
            //Debug.Log(damageTime);
            //Debug.Log(damageTimer);
            damageTimer += Time.deltaTime;

        }
        else
        {
            damageTimer = 0;
        }

        if (totalCollisions == 0)
        {
            damageBool = false;
            thisRB.velocity = new Vector2(Velocity, 0);
        }
        //transform.Translate(velocity*Time.deltaTime, Space.World);
        /*
        if (enemy.position.x >= 5)
        {
            go.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.CompareTag("friendly") || collision.CompareTag("tower"))
        {
            totalCollisions++;
            damageBool = true;
            thisRB.velocity = new Vector2(0.0f, 0.0f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var position = transform.position;
        if (damageTimer >= damageTime && totalCollisions > 0)
        {
            if (collision.gameObject.CompareTag("friendly"))
            {
                damageBool = true;
                damageTimer = 0;
                collision.gameObject.transform.parent.SendMessage("DamageFriendly", damage);
                position += new Vector3(0, -0.2f,0);
                animationStarted = true;
            }else if (collision.gameObject.CompareTag("tower"))
            {
                damageBool = true;
                damageTimer = 0;
                collision.gameObject.SendMessage("DamageTower", damage);
                position += new Vector3(0, -0.2f,0);
                animationStarted = true;
                Destroy(this.gameObject);
            }
        }
        if (animationStarted & damageTimer >= 0.2)
        {
            Debug.Log(this.name);
            position = new Vector2(position.x, origY);
            animationStarted = false;
        }

        transform.position = position;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("friendly") || collision.CompareTag("tower"))
        {
            totalCollisions--;
            if (totalCollisions == 0)
            {
                damageBool = false;
                thisRB.velocity = new Vector2(Velocity, 0);
            }
        }
    }

    public void DamageEnemy(int damagePoints)
    {
        _healthPoint -= damagePoints;
        Debug.Log("some damage took place to enemy" + _healthPoint + ", " + damagePoints);
        //Debug.Log($"Enemy took damage {damage}, HP becomes {_healthPoint}");
        if (_healthPoint <= MinHealthPoint)
        {
        	_healthText.text = "0/" + MaxHealthPoint;
            EnemyWave.spawnedEnemy--;

            // give mana reward
            GiveManaReward();

            //Debug.Log(this.gameObject + "is destroyed.");
            Destroy(gameObject);
            ReportEnemyDeath();
        } else {
        	_healthText.text = _healthPoint + "/" + MaxHealthPoint;
        }
    }

    private void GiveManaReward()
    {
        GameObject resourceManagerObject = GameObject.Find("ResourceManager").gameObject;
        ResourceManager resourceManager = resourceManagerObject.GetComponent<ResourceManager>();

        if (gameObject.name == "BigEnemy") resourceManager.IncreaseMana(BalanceManager.ManaBigEnemyReward);
        else if (gameObject.name == "EnemySlinger") resourceManager.IncreaseMana(BalanceManager.ManaEnemySlingerReward);
        else resourceManager.IncreaseMana(BalanceManager.ManaEnemyReward);
    }

    public void ReportEnemyDeath()
    {
        int timeOfDeath = (int)_elapsedTime % 60;
        Analytics.CustomEvent("EnemyDiedPosition", new Dictionary<string, object>
        {
            { "EnemyDiedPosition", this.gameObject.transform.position.x.ToString("F")},
            { "TimeOfDeath", timeOfDeath }
        });
        Debug.Log("Enemy Position of Death: " + this.gameObject.transform.position.x.ToString("F"));
        Debug.Log("Enemy Time of Death: " + timeOfDeath);
    }
}














