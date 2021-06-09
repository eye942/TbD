using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviourScript : MonoBehaviour
{
    private static readonly int MaxHealthPoint = 100;
    private static readonly int MinHealthPoint = 0;
    private int _healthPoint;
    private GameObject _tower;
    private GameObject[] _levels;
    private GameObject _leftTurret;
    private GameObject _rightTurret;
    private BoxCollider2D _towerBoxCollider;

    // Start is called before the first frame update
    public void Start()
    {
        // initialize tower HP to the max value
        _healthPoint = MaxHealthPoint;

        // Find tower game object
        _tower = gameObject;// GameObject.Find("Tower");
        if (_tower == null) Debug.LogError("Can't find tower");

        // Find levels
        _levels = new GameObject[8];
        for (int i = 0; i < 8; ++i)
        {
            _levels[i] = _tower.transform.Find($"Level{i}").gameObject;
            if (_levels[i] == null) Debug.LogError($"Can't find Level{i}");
        }

        // Find turrets
        _leftTurret = _tower.transform.Find("LeftTurret").gameObject;
        if (_leftTurret == null) Debug.LogError("Can't find LeftTurret");
        _rightTurret = _tower.transform.Find("RightTurret").gameObject;
        if (_rightTurret == null) Debug.LogError("Can't find RightTurret");

        // Get towerBoxCollider
        _towerBoxCollider = gameObject.GetComponent<BoxCollider2D>();

        Debug.Log("Tower initialized");
    }

    // Update is called once per frame
    public void Update()
    {
        // check keyboard input. for testing
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DamageTower(10);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ResetTower();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Fireball")
        {
            Debug.Log($"Tower collided with a Fireball");
            DamageTower(50);
        }
    }

    /// <summary>
    /// Decrease tower HP. Call by the enemy units and enemy projectile objects
    /// </summary>
    /// <param name="damage"></param>
    public void DamageTower(int damage)
    {
        if (_healthPoint - damage < MinHealthPoint) return;
        _healthPoint -= damage;
        Debug.Log($"Tower took damage {damage}, HP becomes {_healthPoint}");
        UpdateTowerAppearance();
    }

    /// <summary>
    /// Reset tower HP. For testing
    /// </summary>
    public void ResetTower()
    {
        _healthPoint = MaxHealthPoint;
        Debug.Log($"Reset Tower, HP becomes {_healthPoint}");
        UpdateTowerAppearance();
    }

    /// <summary>
    /// Update the appearance of the tower, corresponding to current HP value
    /// </summary>
    private void UpdateTowerAppearance()
    {
        int hpFloored = _healthPoint / 10;

        if (hpFloored == 10)
        {
            _leftTurret.SetActive(true);
            _rightTurret.SetActive(true);
            foreach (GameObject level in _levels)
            {
                level.SetActive(true);
            }
        }
        if (hpFloored < 10) _leftTurret.SetActive(false);
        if (hpFloored < 9) _rightTurret.SetActive(false);
        if (hpFloored < 8) _levels[7].SetActive(false);
        if (hpFloored < 7) _levels[6].SetActive(false);
        if (hpFloored < 6) _levels[5].SetActive(false);
        if (hpFloored < 5) _levels[4].SetActive(false);
        if (hpFloored < 4) _levels[3].SetActive(false);
        if (hpFloored < 3) _levels[2].SetActive(false);
        if (hpFloored < 2) _levels[1].SetActive(false);
        if (hpFloored < 1) _levels[0].SetActive(false);
    }

    public int getHealthPoint() {
        return _healthPoint;
    }

}
