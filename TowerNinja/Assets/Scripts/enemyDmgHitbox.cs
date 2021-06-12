using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiate a rigidbody then set the velocity

public class enemyDmgHitbox : MonoBehaviour
{
    public  int MaxHealthPoint = 50;
    private static readonly int MinHealthPoint = 0;
    private int _healthPoint;
    //public GameObject parentOfHitbox;
    public float damageTime;
    public float damageTimer;
    public int damage;
    //checks if the enemy is doing damage
    private bool damageBool = false;

    void Start()
    {
        _healthPoint = MaxHealthPoint;
        damage = 10;
        damageTime = 1.5f;
        damageTimer = 0.0f;
    }
    void Update()
    {

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
        Debug.Log(collision.tag);
        if (collision.tag == "friendly")
        {
            //Debug.Log("proc");
            damageBool = true;
            //Debug.Log(collision.gameObject);
            //Debug.Log(collision.gameObject.transform.parent);
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
            //Die();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("stay");
        //Debug.Log(damageTimer);
        if (collision.tag == "friendly")
        {
            //Debug.Log(collision.gameObject);
            //Debug.Log(damageTimer);
            damageBool = true; 
        }
        if (damageTimer >= damageTime) // && collision.tag == "friendly"
        {
            if (collision.tag == "friendly")
            {
                damageTimer = 0;
                /*
                Debug.Log(collision.gameObject);
                Debug.Log(collision.gameObject.transform.parent);
                */
                collision.gameObject.SendMessage("DamageTower", damage);

                Debug.Log(damage);
                collision.gameObject.transform.parent.SendMessage("DamageFriendly", 10);
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "friendly")
        {
            damageTimer = 0;
            //Debug.Log("leave");
            damageBool = false;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(1.5f, 0.0f);
        }
    }

    public void DamageEnemy(int damage)
    {

        _healthPoint -= damage;
        Debug.Log($"Enemy took damage {damage}, HP becomes {_healthPoint}");
        if (_healthPoint <= MinHealthPoint)
        {
            Destroy(this.gameObject);
        }
    }
}














