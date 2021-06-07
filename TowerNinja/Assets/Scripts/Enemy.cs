using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiate a rigidbody then set the velocity

public class Enemy : MonoBehaviour
{
    public Transform prefab;
    public Vector3 velocity;
    public Transform enemy;
    public GameObject go;

    void Start()
    {
    	velocity.x = 1.5f;
        velocity.y = 0;
        velocity.z = 0;

        enemy = Instantiate(prefab, new Vector3(-5, -3, 0), Quaternion.identity);
        Debug.Log("lets go");
        go = enemy.gameObject;
        go.AddComponent<Rigidbody2D>();
        go.GetComponent<Rigidbody2D>().gravityScale = 0;
        go.GetComponent<Rigidbody2D>().velocity = velocity;

    	//enemy.rigidbody.velocity = velocity;
    }
    void Update()
    {
    	//transform.Translate(velocity*Time.deltaTime, Space.World);
        /*
    	if (enemy.position.x >= 5) {
    		go.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    	}
        */
    }


}














