using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    
    private int _healthPoint;
    private GameObject _tower;
    private GameObject[] _levels;
    private GameObject _passiveAttackSpawn;
    private BoxCollider2D _towerBoxCollider;
    private GameObject _topSection;

    // Start is called before the first frame update
    public void Start()
    {
        // initialize tower HP to the max value
        _healthPoint = BalanceManager.TowerMaxHealthPoint;

        // Find tower game object
        _tower = gameObject;// GameObject.Find("Tower");
        if (_tower == null) Debug.LogError("Can't find tower");

        // Find levels
        _levels = new GameObject[10];
        for (int i = 0; i < 10; ++i)
        {
            _levels[i] = _tower.transform.Find($"Level{i}").gameObject;
            if (_levels[i] == null) Debug.LogError($"Can't find Level{i}");
        }

        _topSection = _levels[9];

        // Get towerBoxCollider
        _towerBoxCollider = gameObject.GetComponent<BoxCollider2D>();

        // Get passiveAttackSpawn
        _passiveAttackSpawn = _tower.transform.Find("PassiveAttackSpawn").gameObject;

        Debug.Log("Tower initialized");
    }

    // Update is called once per frame
    public void Update()
    {
        // check keyboard input. for testing
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DamageTower(BalanceManager.TowerTestKeyboardValue);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ResetTower();
        }
    }

    /// <summary>
    /// Decrease tower HP. Call by the enemy units and enemy projectile objects
    /// </summary>
    /// <param name="damage"></param>
    public void DamageTower(int damage)
    {
        _healthPoint -= damage;
        UpdateTowerAppearance();
        Debug.Log($"Tower took damage {damage}, HP becomes {_healthPoint}");

        if (_healthPoint <= BalanceManager.TowerMinHealthPoint)
        {
            GameOver();
            Debug.Log("GameOver");
        }
    }

    /// <summary>
    /// Reset tower HP. For testing
    /// </summary>
    public void ResetTower()
    {
        _healthPoint = BalanceManager.TowerMaxHealthPoint;
        UpdateTowerAppearance();
        Debug.Log($"Reset Tower, HP becomes {_healthPoint}");
    }

    /// <summary>
    /// Update the appearance of the tower, corresponding to current HP value
    /// </summary>
    private void UpdateTowerAppearance()
    {
        // set visibility of each level
        if (_healthPoint > (0.9 * BalanceManager.TowerMaxHealthPoint))
        {
            _topSection = _levels[9];
            foreach (GameObject level in _levels)
            {
                level.SetActive(true);
            }
            UpdateTowerColliderShape(8.2f - (0.75f * 0), -3.4f - (0.375f * 0));
            _passiveAttackSpawn.SendMessage("EnableArrowSpawn");
            _passiveAttackSpawn.SendMessage("EnableBombSpawn");
        }
        if (_healthPoint <= (0.9 * BalanceManager.TowerMaxHealthPoint))
        {
            _levels[9].SetActive(false);
            _topSection = _levels[8];
            UpdateTowerColliderShape(8.2f - (0.75f * 1), -3.4f - (0.375f * 1));
        }
        if (_healthPoint <= (0.8 * BalanceManager.TowerMaxHealthPoint))
        {
            _levels[8].SetActive(false);
            _topSection = _levels[7];
            UpdateTowerColliderShape(8.2f - (0.75f * 2), -3.4f - (0.375f * 2));
            _passiveAttackSpawn.SendMessage("DisableArrowSpawn");
        }
        if (_healthPoint <= (0.7 * BalanceManager.TowerMaxHealthPoint))
        {
            _levels[7].SetActive(false);
            _topSection = _levels[6];
            UpdateTowerColliderShape(8.2f - (0.75f * 3), -3.4f - (0.375f * 3));
        }
        if (_healthPoint <= (0.6 * BalanceManager.TowerMaxHealthPoint))
        {
            _levels[6].SetActive(false);
            _topSection = _levels[5];
            UpdateTowerColliderShape(8.2f - (0.75f * 4), -3.4f - (0.375f * 4));
        }
        if (_healthPoint <= (0.5 * BalanceManager.TowerMaxHealthPoint))
        {
            _levels[5].SetActive(false);
            _topSection = _levels[4];
            UpdateTowerColliderShape(8.2f - (0.75f * 5), -3.4f - (0.375f * 5));
        }
        if (_healthPoint <= (0.4 * BalanceManager.TowerMaxHealthPoint))
        {
            _levels[4].SetActive(false);
            _topSection = _levels[3];
            UpdateTowerColliderShape(8.2f - (0.75f * 6), -3.4f - (0.375f * 6));
            _passiveAttackSpawn.SendMessage("DisableBombSpawn");
        }
        if (_healthPoint <= (0.3 * BalanceManager.TowerMaxHealthPoint))
        {
            _levels[3].SetActive(false);
            _topSection = _levels[2];
            UpdateTowerColliderShape(8.2f - (0.75f * 7), -3.4f - (0.375f * 7));
        }
        if (_healthPoint <= (0.2 * BalanceManager.TowerMaxHealthPoint))
        {
            _levels[2].SetActive(false);
            _topSection = _levels[1];
            UpdateTowerColliderShape(8.2f - (0.75f * 8), -3.4f - (0.375f * 8));
        }
        if (_healthPoint <= (0.1 * BalanceManager.TowerMaxHealthPoint))
        {
            _levels[1].SetActive(false);
            _topSection = _levels[0];
            UpdateTowerColliderShape(8.2f - (0.75f * 9), -3.4f - (0.375f * 9));
        }
        if (_healthPoint <= BalanceManager.TowerMinHealthPoint)
        {
            _levels[0].SetActive(false);
            Destroy(this.gameObject);
        }
    }

    private void UpdateTowerColliderShape(float sizeY, float offsetY)
    {
        _towerBoxCollider.size = new Vector2(2.2f, sizeY);
        _towerBoxCollider.offset = new Vector2(-3.6f, offsetY);
    }


    public int GetHealthPoint()
    {
        return _healthPoint;
    }

    public void GameOver()
    {
        SceneChanger.GameOver();
    }

}
