using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiate a rigidbody then set the velocity

public class Friend : MonoBehaviour
{
    
    public Transform prefab;
    public Vector3 velocity;
    public Transform friend;
    public GameObject go;

    void Start()
    {
        
        velocity.x = -1.5f;
        velocity.y = 0;
        velocity.z = 0;

        friend = Instantiate(prefab, this.transform.position, Quaternion.identity) ;
        Debug.Log("lets go");
        go = friend.gameObject;
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














