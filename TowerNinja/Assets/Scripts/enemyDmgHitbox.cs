using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private static readonly int BigEnemyManaReward = 20;
    private static readonly int EnemyManaReward = 10;

    void Start()
    {
        _elapsedTime += Time.deltaTime;

        totalCollisions = 0;
        if (EnemyWave.numWaves > 5)
        {
            _healthPoint = Mathf.RoundToInt(MaxHealthPoint * (EnemyWave.numWaves - 5) * 1.1f);
            damage = Mathf.RoundToInt(damage * (EnemyWave.numWaves - 5) * 1.1f);
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
        if (collision.tag == "friendly")
        {
            totalCollisions++;
            damageBool = true;
            thisRB.velocity = new Vector2(0.0f, 0.0f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (damageTimer >= damageTime && totalCollisions > 0 && collision.gameObject.tag == "friendly")
        {
            damageBool = true;
            damageTimer = 0;
            collision.gameObject.SendMessage("DamageTower", damage);
            collision.gameObject.transform.parent.SendMessage("DamageFriendly", damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "friendly")
        {
            totalCollisions--;
            if (totalCollisions == 0)
            {
                damageBool = false;
                thisRB.velocity = new Vector2(Velocity, 0);
            }
        }
    }

    public void DamageEnemy(int damage)
    {

        _healthPoint -= damage;
        //Debug.Log($"Enemy took damage {damage}, HP becomes {_healthPoint}");
        if (_healthPoint <= MinHealthPoint)
        {
            EnemyWave.spawnedEnemy--;

            // give mana reward
            GiveManaReward();

            //Debug.Log(this.gameObject + "is destroyed.");
            Destroy(this.gameObject);
            ReportEnemyDeath();
        }
    }

    private void GiveManaReward()
    {
        GameObject resourceManagerObject = GameObject.Find("ResourceManager").gameObject;
        ResourceManager resourceManager = resourceManagerObject.GetComponent<ResourceManager>();

        if (gameObject.name.StartsWith("Big"))
        {
            resourceManager.IncreaseMana(BigEnemyManaReward);
        }
        else
        {
            resourceManager.IncreaseMana(EnemyManaReward);
        }
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














