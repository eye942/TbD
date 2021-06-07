using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiate a rigidbody then set the velocity

public class enemyDmgHitbox : MonoBehaviour
{
    public GameObject parentOfHitbox;
    public float damageTime;
    public float damageTimer;
    private int damage;
    //checks if the enemy is doing damage
    private bool damageBool = false;

    void Start()
    {
        damage = 10;
        damageTime = 0.8f;
        damageTimer = 0.0f;
    }
    void Update()
    {

        if (damageBool)
        {
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
        Debug.Log("proc");
        damageBool = true;
        this.parentOfHitbox.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        //Die();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("stay");
        //Debug.Log(damageTimer);
        if (damageTimer >= damageTime)
        {
            damageTimer = 0;
            collision.gameObject.SendMessage("DamageTower", damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("leave");
        damageBool = false; 
        this.parentOfHitbox.GetComponent<Rigidbody2D>().velocity = new Vector2(1.5f, 0.0f);
    }    
}














