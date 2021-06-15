using UnityEngine;
using System.Collections.Generic;

public class ArrowSpawn : MonoBehaviour
{
	private float _lastSpawnTime;
	private bool _spawn = true;
	private int _spawnID;
	private static readonly int _spawnCooldown = 1;
	public GameObject _arrow;
	
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
		GameObject newArrow = Instantiate(_arrow, gameObject.transform.position, Quaternion.identity);
	}
}
