using UnityEngine;

public class PassiveAttackSpawn : MonoBehaviour
{
    

    private float _arrowLastSpawnTime;
    private float _bombLastSpawnTime;
    private bool _spawnArrow = true;
    private bool _spawnBomb = true;

    private static readonly Vector2 DefaultArrowSpawnPosition = new Vector2(5, 2);
    private static readonly Vector2 DefaultBombSpawnPosition = new Vector2(5, -1);

    private Vector2 _arrowSpawnPosition;
    private Vector2 _bombSpawnPosition;

    public GameObject Arrow;
    public GameObject Bomb;

    void Start()
    {
        _arrowLastSpawnTime = BalanceManager.ArrowSpawnCooldown;
        _bombLastSpawnTime = BalanceManager.BombSpawnCooldown;
        _arrowSpawnPosition = DefaultArrowSpawnPosition;
        _bombSpawnPosition = DefaultBombSpawnPosition;
    }

    void Update()
    {
        if (_spawnArrow)
        {
            _arrowLastSpawnTime += Time.deltaTime;
            if (_arrowLastSpawnTime > BalanceManager.ArrowSpawnCooldown)
            {
                SpawnArrow();
                _arrowLastSpawnTime = 0;
            }
        }

        if (_spawnBomb)
        {
            _bombLastSpawnTime += Time.deltaTime;
            if (_bombLastSpawnTime > BalanceManager.BombSpawnCooldown)
            {
                SpawnBomb();
                _bombLastSpawnTime = 0;
            }
        }
    }

    private void SpawnArrow()
    {
        GameObject newArrow = Instantiate(Arrow, _arrowSpawnPosition, Quaternion.identity);
    }

    private void SpawnBomb()
    {
        GameObject newBomb = Instantiate(Bomb, _bombSpawnPosition, Quaternion.identity);
    }

    public void DisableArrowSpawn()
    {
        _spawnArrow = false;
    }

    public void EnableArrowSpawn()
    {
        _spawnArrow = true;
    }

    public void DisableBombSpawn()
    {
        _spawnBomb = false;
    }

    public void EnableBombSpawn()
    {
        _spawnBomb = true;
    }
}
