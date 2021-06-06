using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiate a rigidbody then set the velocity

public class ExampleClass : MonoBehaviour
{
    public Transform prefab;
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
        }
    }
}