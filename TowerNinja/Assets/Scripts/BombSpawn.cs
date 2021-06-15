using UnityEngine;

public class BombSpawn : MonoBehaviour
{
	private float _lastSpawnTime;
	private bool _spawn = true;
	private int _spawnID;
	private static readonly int _spawnCooldown = 3;
	public GameObject _bomb;

	void Start()
	{
		// sets a random number for the id of the spawner
		_spawnID = Random.Range(1, 500);
		_lastSpawnTime = _spawnCooldown;
	}

	void Update()
	{
		if (_spawn)
		{
			_lastSpawnTime += Time.deltaTime;
			if (_lastSpawnTime > _spawnCooldown)
			{
				Spawn();
				_lastSpawnTime = 0;
			}
		}
	}

	private void Spawn()
	{
		GameObject newBomb = Instantiate(_bomb, gameObject.transform.position, Quaternion.identity);
	}
}
