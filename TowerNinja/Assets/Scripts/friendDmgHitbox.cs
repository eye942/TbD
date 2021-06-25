using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiate a rigidbody then set the velocity

public class friendDmgHitbox : MonoBehaviour
{
    private Rigidbody2D thisRB;
    public int MaxHealthPoint = 50;
    private static readonly int MinHealthPoint = 0;
    private int _healthPoint;
    //public GameObject parentOfHitbox;
    public float damageTime;
    public float damageTimer;
    public int damage;
    //checks if the enemy is doing damage
    private bool damageBool = false;
    private Vector2 velocity;
    private int totalCollisions;
    
    void Start()
    {
        thisRB = this.GetComponent<Rigidbody2D>();
        totalCollisions = 0;
        _healthPoint = MaxHealthPoint;
        damageTimer = 0.0f;
        velocity = thisRB.velocity;
    }
    void Update()
    {
        Debug.Log("Total Collisions: " + totalCollisions);

        if (damageBool)
        {
            damageTimer += Time.deltaTime;

        }
        else
        {
            damageTimer = 0;
        }

        if (totalCollisions == 0)
        {
            damageBool = false;
            thisRB.velocity = velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "enemy")
        {
            totalCollisions++;
            damageBool = true;
            thisRB.velocity = new Vector2(0.0f, 0.0f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (damageTimer >= damageTime && totalCollisions > 0 && collision.gameObject.tag == "enemy")
        {
            damageBool = true;
            damageTimer = 0;
            Debug.Log(collision.gameObject);
            collision.gameObject.transform.parent.SendMessage("DamageEnemy", damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            totalCollisions--;
            Debug.Log("leave");
            //set collision check
            if (totalCollisions == 0)
            {
                damageBool = false;
                thisRB.velocity = velocity;
            }
        }
        
    }

    public void DamageFriendly(int damage)
    {
        
        _healthPoint -= damage;
        Debug.Log($"Friendly took damage {damage}, HP becomes {_healthPoint}");
        if (_healthPoint <= MinHealthPoint)
        {
            Debug.Log(this.gameObject + "is destroyed.");
            Destroy(this.gameObject);
        }
    }
}














