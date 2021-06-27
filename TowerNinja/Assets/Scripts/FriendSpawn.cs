using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiate a rigidbody then set the velocity

public class FriendSpawn : MonoBehaviour
{
    public Transform prefab;
    public Vector3 velocity;
    public Transform friend;
    public GameObject go;
    public KeyCode activateKey;

    private ResourceManager resourceManager;
    private static readonly int BigFriendManaCost = 25;
    private static readonly int FriendManaCost = 15;
    private bool spawningBigFriend;

    void Start()
    {
        // Get resource manager
        GameObject resourceManagerObject = GameObject.Find("ResourceManager").gameObject;
        resourceManager = resourceManagerObject.GetComponent<ResourceManager>();

        // spawning big friend?
        if (prefab.name == "BigFriend") spawningBigFriend = true;
        else spawningBigFriend = false; // spwaning regular friend

        /*
        friend = Instantiate(prefab, this.transform.position, Quaternion.identity) ;
        Debug.Log("lets go");
        go = friend.gameObject;
        go.GetComponent<Rigidbody2D>().gravityScale = 0;
        go.GetComponent<Rigidbody2D>().velocity = velocity;
        */
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
        if (Input.GetKeyDown(activateKey))
        {
            // check if we have enough mana to spawn new friendly units
            if (HaveEnoughMana())
            {
                friend = Instantiate(prefab, this.transform.position, Quaternion.identity);
                ConsumeMana();
                //Debug.Log("lets go");
                go = friend.gameObject;
                go.GetComponent<Rigidbody2D>().gravityScale = 0;
                go.GetComponent<Rigidbody2D>().velocity = velocity;
            }
        }
    }

    private bool HaveEnoughMana()
    {
        if (spawningBigFriend && resourceManager.GetManaCount() >= BigFriendManaCost)
        {
            return true;
        }
        else if (!spawningBigFriend && resourceManager.GetManaCount() >= FriendManaCost)
        {
            return true;
        }
        return false;
    }

    private void ConsumeMana()
    {
        if (spawningBigFriend) resourceManager.DecreaseMana(BigFriendManaCost);
        else resourceManager.DecreaseMana(FriendManaCost);
    }

}














