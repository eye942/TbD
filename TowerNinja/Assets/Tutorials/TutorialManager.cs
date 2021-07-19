using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    public GameObject arrowBombSpawner;
    public GameObject fireballSpawner;
    public GameObject enemySpawner;
    public GameObject friendlySpawner;

    void Awake()
    {
        arrowBombSpawner.SetActive(false);
        fireballSpawner.SetActive(false);
        enemySpawner.SetActive(false);
        friendlySpawner.SetActive(false);
    }

    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        if (popUpIndex == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
                Debug.Log("Tutorial Begins." + popUpIndex);
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                arrowBombSpawner.SetActive(true);
                popUpIndex++;
                Debug.Log("Arrow & Bomb Activated" + popUpIndex);
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                fireballSpawner.SetActive(true);
                popUpIndex++;
                Debug.Log("Fireball activated.");
                StartCoroutine(WaitForSomeTime());
                WaitForSomeTime();           
            }
        }
        else if (popUpIndex == 3)
        {
            if (fireballSpawner.activeSelf == false)
            {
                enemySpawner.SetActive(true);
                friendlySpawner.SetActive(true);
                popUpIndex++;
                Debug.Log("Enemy + Friendly activated.");
                StartCoroutine(WaitForAllEnemiesDie());
                WaitForAllEnemiesDie();
            }
        } 
        else if (popUpIndex == 4)
        {
            if (enemySpawner.activeSelf == false)
            {
                popUpIndex++;   
            }         
        }       
    }

    IEnumerator WaitForSomeTime()
    {
        yield return new WaitForSeconds(10);
        fireballSpawner.SetActive(false);
        Debug.Log("Fireball De-activated.");
    }

    IEnumerator WaitForAllEnemiesDie()
    {
        yield return new WaitForSeconds(20);
        enemySpawner.SetActive(false);
        Debug.Log("Enmemy De-activated.");
    }
}

