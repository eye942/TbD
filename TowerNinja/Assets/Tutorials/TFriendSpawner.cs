using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiate a rigidbody then set the velocity

public class TFriendSpawner : MonoBehaviour
{
    public Transform prefab;
    public Vector3 velocity;
    public Transform friend;
    public GameObject go;
    public KeyCode activateKey;
    private TutorialRM resourceManager;
    private int _friendManaCost;

    void Start()
    {
        // Get resource manager
        GameObject resourceManagerObject = GameObject.Find("TutorialRM");
        resourceManager = resourceManagerObject.GetComponent<TutorialRM>();

        // set mana cost for this spawner
        if (prefab.name == "BigFriend") _friendManaCost = BalanceManager.ManaBigFriendCost;
        else if (prefab.name == "FriendSlinger") _friendManaCost = BalanceManager.ManaFriendSlingerCost;
        else _friendManaCost = BalanceManager.ManaFriendCost;

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
}