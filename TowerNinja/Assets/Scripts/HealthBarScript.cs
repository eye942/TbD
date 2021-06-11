using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{

	private TowerBehaviourScript tbs;
	private int _healthPoints; 
    // Start is called before the first frame update
    public void Start()
    {
    	GameObject _tower = GameObject.Find("Tower");
		tbs = _tower.GetComponent<TowerBehaviourScript>();

    }

    // Update is called once per frame
    public void Update()
    {
        float _fullHealth = 100;

        _healthPoints = tbs.getHealthPoint();
        Debug.Log("health=" + _healthPoints/_fullHealth);
        GetComponent<Renderer>().material.color = new Color(_healthPoints/_fullHealth, 0, 0);
    }	
}
