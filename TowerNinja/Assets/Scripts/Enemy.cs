using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiate a rigidbody then set the velocity

public class Enemy : MonoBehaviour
{
    private static readonly int MaxHealthPoint = 50;
    private static readonly int MinHealthPoint = 0;
    public Transform prefab;
    public Vector3 velocity;
    public Transform enemy;
    public GameObject go;
    public int spawnNum;
    private int totalNumSpawned = 0;
    public float spawnTimer;

    private float timer = 0.0f; 

    void Start()
    {
    	velocity.x = 1.5f;
        velocity.y = 0;
        velocity.z = 0;
        /*
        enemy = Instantiate(prefab, new Vector3(-5, -3, 0), Quaternion.identity);
        Debug.Log("lets go");
        go = enemy.gameObject;
        go.GetComponent<Rigidbody2D>().gravityScale = 0;
        go.GetComponent<Rigidbody2D>().velocity = velocity;
        */
    	//enemy.rigidbody.velocity = velocity;
    }
    void Update()
    {
        timer += Time.deltaTime; 
        if ((timer >= spawnTimer) && (spawnNum > totalNumSpawned))
        {
            totalNumSpawned++;
            timer = 0.0f;
            enemy = Instantiate(prefab, new Vector3(-5, -3, 0), Quaternion.identity);
            Debug.Log("lets go");
            go = enemy.gameObject;
            go.GetComponent<Rigidbody2D>().gravityScale = 0;
            go.GetComponent<Rigidbody2D>().velocity = velocity;

        }
    	//transform.Translate(velocity*Time.deltaTime, Space.World);
        /*
    	if (enemy.position.x >= 5) {
    		go.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    	}
        */
    }


}














