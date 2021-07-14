using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using TMPro;

// Instantiate a rigidbody then set the velocity

public class friendDmgHitbox : MonoBehaviour
{
    public string friendType;
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
    public TMPro.TextMeshPro _healthText;
    GameObject textobj;

    private float origY;
    bool animationStarted = false;
    void Start()
    {
        textobj = this.gameObject.transform.GetChild (2).gameObject;
        _healthText = textobj.GetComponent<TextMeshPro>();
        _healthText.text = MaxHealthPoint + "/" + MaxHealthPoint;

        AnalyticsEvent.ItemAcquired(AcquisitionType.Soft,"Mana Store",1, friendType, "Unit", $"{Time.fixedTime}");
        thisRB = this.GetComponent<Rigidbody2D>();
        totalCollisions = 0;
        _healthPoint = MaxHealthPoint;
        damageTimer = 0.0f;
        velocity = thisRB.velocity;
        origY = this.transform.position.y;
        //origY = -3.3f;
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
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.2f);
            animationStarted = true; 
        }
        if (animationStarted & damageTimer >= 0.2)
        {
            Debug.Log(this.name);
            this.transform.position = new Vector2(this.transform.position.x, origY);
            animationStarted = false;
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
                this.transform.position = new Vector2(this.transform.position.x, origY);
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
            _healthText.text = "0/" + MaxHealthPoint;
            Debug.Log(this.gameObject + "is destroyed.");
            AnalyticsEvent.ItemSpent(AcquisitionType.Soft,"Mana Store",1, friendType, "Unit", $"{Time.fixedTime}");
            Destroy(this.gameObject);
            ReportFriendlyDeath();
        } else {
            _healthText.text = _healthPoint + "/" + MaxHealthPoint;
        }
    }

    public void ReportFriendlyDeath()
    {
        Analytics.CustomEvent("FriendlyDiedPosition", new Dictionary<string, object>
            {
                { "FriendlyDiedPosition", this.gameObject.transform.position.x.ToString("F")},
            });
        Debug.Log("Friendly Position of Death: " + this.gameObject.transform.position.x.ToString("F"));

    }
}














