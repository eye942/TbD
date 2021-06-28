using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

	private Tower tbs;
	private int _healthPoints; 
    // Start is called before the first frame update
    public void Start()
    {
    	GameObject _tower = GameObject.Find("Tower");
		tbs = _tower.GetComponent<Tower>();

    }

    // Update is called once per frame
    public void Update()
    {
        float _fullHealth = 100;

        _healthPoints = tbs.GetHealthPoint();
        Debug.Log("health=" + _healthPoints/_fullHealth);
        GetComponent<Renderer>().material.color = new Color(_healthPoints/_fullHealth, 0, 0);
   
    }	
}
