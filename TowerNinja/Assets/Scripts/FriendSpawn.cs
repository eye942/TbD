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
    private int _friendManaCost;
    private int spawnCode;   //std = 1, big = 2, slinger = 3

    void Start()
    {
        // Get resource manager
        GameObject resourceManagerObject = GameObject.Find("ResourceManager");
        resourceManager = resourceManagerObject.GetComponent<ResourceManager>();

        // set mana cost for this spawner
        if (prefab.name == "BigFriend") {
            _friendManaCost = BalanceManager.ManaBigFriendCost;
            spawnCode= 2;
        }
        else if (prefab.name == "FriendSlinger"){
             _friendManaCost = BalanceManager.ManaFriendSlingerCost;
             spawnCode= 3;
        }
        else {
            _friendManaCost = BalanceManager.ManaFriendCost;
            spawnCode= 1;
            }


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
            if (HaveEnoughMana()&& CanSpawn())
            {
                Spawn();
                friend = Instantiate(prefab, this.transform.position, Quaternion.identity);
                resourceManager.DecreaseMana(_friendManaCost);
                //Debug.Log("lets go");
                go = friend.gameObject;
                go.GetComponent<Rigidbody2D>().gravityScale = 0;
                go.GetComponent<Rigidbody2D>().velocity = velocity;
            }
        }
    }

    private bool HaveEnoughMana()
    {
        if (resourceManager.GetManaCount() >= _friendManaCost) return true;
        return false;
    }

    private bool CanSpawn()
    {
            if (spawnCode == 1)
                return resourceManager.CanSpawnFriendly();
            if(spawnCode== 2)
                return resourceManager.CanSpawnBigFriendly();
            return resourceManager.CanSpawnSlingerFriendly();
    }
    private void Spawn()
    {
            if (spawnCode == 1)
                resourceManager.SpawnFriendly();
            else if(spawnCode== 2)
                resourceManager.SpawnBigFriendly();
            else
                resourceManager.SpawnSlingerFriendly();
    }
}














