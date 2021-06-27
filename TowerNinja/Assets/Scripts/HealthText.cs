using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{

	private Tower tbs;
	private int _healthPoints; 
	public Text _healthText;

    // Start is called before the first frame update
    void Start()
    {
    	GameObject _tower = GameObject.Find("Tower");
		tbs = _tower.GetComponent<Tower>();
    	_healthText = GameObject.Find("HealthText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _healthPoints = tbs.GetHealthPoint();
        _healthText.text = _healthPoints + " / 100";
    }

}