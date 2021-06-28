using UnityEngine;
using System.Collections.Generic;

public class EnemyWave : MonoBehaviour
{
	// Color of the gizmo
	public Color gizmoColor = Color.red;

	//-----------------------------------
	// All the Enums
	//-----------------------------------
	// Spawn types
	public enum SpawnTypes
	{
		Normal,
		Once,
		Wave,
		TimedWave
	}
	// The different Enemy levels
	public enum EnemyLevels
	{
		Easy,
		Medium,
		Hard,
		Boss
	}
	//---------------------------------
	// End of the Enums
	//---------------------------------

	// Enemy level to be spawnedEnemy
	public EnemyLevels enemyLevel = EnemyLevels.Easy;

	//----------------------------------
	// Enemy Prefabs
	//----------------------------------
	public GameObject MiniEnemy;
	public GameObject BigEnemy;
	public GameObject Slinger;
	private Dictionary<EnemyLevels, GameObject> Enemies = new Dictionary<EnemyLevels, GameObject>(3);

	private float lastSpawnTime;
	//----------------------------------
	// End of Enemy Prefabs
	//----------------------------------

	//----------------------------------
	// Enemies and how many have been created and how many are to be created
	//----------------------------------
	public int totalEnemy = 10;
	private int numEnemy = 0;
	public static int spawnedEnemy = 0;
	//----------------------------------
	// End of Enemy Settings
	//----------------------------------


	// The ID of the spawner
	private int SpawnID;

	//----------------------------------
	// Different Spawn states and ways of doing them
	//----------------------------------
	private bool waveSpawn = false;
	public bool Spawn = true;
	public SpawnTypes spawnType = SpawnTypes.Normal;
	// timed wave controls
	//public float waveTimer = 30.0f;
	private float timeTillWave = 0.0f;
	private float timer = 0.0f;
	//Wave controls
	public int totalWaves = 5;
	public static int numWaves = 1;
	private int waveReps = 0;
	private bool clusterMode = false;
	private int totalNonClusterSpawned = 0;
	private int repetitions = 0; 
	//----------------------------------
	// End of Different Spawn states and ways of doing them
	//----------------------------------
	void Start()
	{
		// sets a random number for the id of the spawner
		//SpawnID = Random.Range(1, 500);
		//Enemies.Add(EnemyLevels.Easy, EasyEnemy);
		//Enemies.Add(EnemyLevels.Boss, BossEnemy);
		//Enemies.Add(EnemyLevels.Medium, MediumEnemy);
		//Enemies.Add(EnemyLevels.Hard, HardEnemy);
		//lastSpawnTime = 3f;
	}
	// Draws a cube to show where the spawn point is... Useful if you don't have a object that show the spawn point
	void OnDrawGizmos()
	{
		// Sets the color to red
		Gizmos.color = gizmoColor;
		//draws a small cube at the location of the game object that the script is attached to
		Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
	}
	void Update()
	{
		Debug.Log("Wave Number: " + numWaves);
		timer += Time.deltaTime;
		if (numWaves == 1)
		{
			if (waveReps != 3)
			{
				//Loop below thrice then increment wave counter
				// 5 minis
				if ((timer >= 5.5f) && (3 > totalNonClusterSpawned) && (clusterMode == false))
				{
					totalNonClusterSpawned++;
					Instantiate(MiniEnemy, new Vector3(this.transform.position.x, -3, 0), Quaternion.identity);
					Debug.Log("lets go");
					timer = 0.0f;
					//go = enemy.gameObject;
					//go.GetComponent<Rigidbody2D>().gravityScale = 0;
					//go.GetComponent<Rigidbody2D>().velocity = velocity;
				}
				else if ((clusterMode == false) && (3 <= totalNonClusterSpawned))
				{
					//the clustermode changes so that we start spawning the cluster, switch back after cluster is spawned
					clusterMode = true;
				}

				if ((timer >= 5.5f) && (clusterMode == true))
				{
					timer = 0.0f;
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					Instantiate(MiniEnemy, new Vector3(this.transform.position.x - 1, -3, 0), Quaternion.identity);
					Instantiate(MiniEnemy, new Vector3(this.transform.position.x, -3, 0), Quaternion.identity);
					Instantiate(MiniEnemy, new Vector3(this.transform.position.x + 1, -3, 0), Quaternion.identity);
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					clusterMode = false;
					totalNonClusterSpawned = 0;
					waveReps++;
					// cluster
				}
			}
			else
			{
				numWaves++;
				waveReps = 0;
			}
			/*
			else if (spawnedEnemy == 0)
			{
				numWaves++;
				waveReps = 0;
			}
			*/
		}
		else if (numWaves == 2)
		{
			if (waveReps != 3)
			{
				//Loop below thrice then increment wave counter
				// 5 minis
				if ((timer >= 5.5f) && (3 > totalNonClusterSpawned) && (clusterMode == false))
				{
					totalNonClusterSpawned++;
					Instantiate(MiniEnemy, new Vector3(this.transform.position.x, -3, 0), Quaternion.identity);
					Debug.Log("lets go");
					timer = 0.0f;
					//go = enemy.gameObject;
					//go.GetComponent<Rigidbody2D>().gravityScale = 0;
					//go.GetComponent<Rigidbody2D>().velocity = velocity;
				}
				else if ((clusterMode == false) && (3 <= totalNonClusterSpawned))
				{
					//the clustermode changes so that we start spawning the cluster, switch back after cluster is spawned
					clusterMode = true;
				}

				if ((timer >= 5.5f) && (clusterMode == true))
				{
					timer = 0.0f;
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					Instantiate(MiniEnemy, new Vector3(this.transform.position.x - 1, -3, 0), Quaternion.identity);
					Instantiate(BigEnemy, new Vector3(this.transform.position.x, -3, 0), Quaternion.identity);
					Instantiate(MiniEnemy, new Vector3(this.transform.position.x + 1, -3, 0), Quaternion.identity);
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					clusterMode = false;
					totalNonClusterSpawned = 0;
					waveReps++;
					// cluster
				}
			}
			else
			{
				numWaves++;
				waveReps = 0;
			}
		}
		else if (numWaves == 3)
		{
			if (waveReps != 3)
			{
				//Loop below thrice then increment wave counter
				// 5 minis
				if ((timer >= 5.5f) && (3 > totalNonClusterSpawned) && (clusterMode == false))
				{
					totalNonClusterSpawned++;
					Instantiate(MiniEnemy, new Vector3(this.transform.position.x, -3, 0), Quaternion.identity);
					Debug.Log("lets go");
					timer = 0.0f;
					//go = enemy.gameObject;
					//go.GetComponent<Rigidbody2D>().gravityScale = 0;
					//go.GetComponent<Rigidbody2D>().velocity = velocity;
				}
				else if ((clusterMode == false) && (3 <= totalNonClusterSpawned))
				{
					//the clustermode changes so that we start spawning the cluster, switch back after cluster is spawned
					clusterMode = true;
				}

				if ((timer >= 5.5f) && (clusterMode == true))
				{
					timer = 0.0f;
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					Instantiate(BigEnemy, new Vector3(this.transform.position.x - 1, -3, 0), Quaternion.identity);
					Instantiate(BigEnemy, new Vector3(this.transform.position.x, -3, 0), Quaternion.identity);
					Instantiate(MiniEnemy, new Vector3(this.transform.position.x + 1, -3, 0), Quaternion.identity);
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					clusterMode = false;
					totalNonClusterSpawned = 0;
					waveReps++;
					// cluster
				}
			}
			else
			{
				numWaves++;
				waveReps = 0;
			}
		}
		else if (numWaves == 4)
		{
			if (waveReps != 3)
			{
				//Loop below thrice then increment wave counter
				// 5 minis
				if ((timer >= 5.5f) && (3 > totalNonClusterSpawned) && (clusterMode == false))
				{
					totalNonClusterSpawned++;
					Instantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					Debug.Log("lets go");
					timer = 0.0f;
					//go = enemy.gameObject;
					//go.GetComponent<Rigidbody2D>().gravityScale = 0;
					//go.GetComponent<Rigidbody2D>().velocity = velocity;
				}
				else if ((clusterMode == false) && (3 <= totalNonClusterSpawned))
				{
					//the clustermode changes so that we start spawning the cluster, switch back after cluster is spawned
					clusterMode = true;
				}

				if ((timer >= 5.5f) && (clusterMode == true))
				{
					timer = 0.0f;
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					Instantiate(BigEnemy, new Vector3(this.transform.position.x - 1, -3, 0), Quaternion.identity);
					Instantiate(BigEnemy, new Vector3(this.transform.position.x, -3, 0), Quaternion.identity);
					Instantiate(BigEnemy, new Vector3(this.transform.position.x + 1, -3, 0), Quaternion.identity);
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					clusterMode = false;
					totalNonClusterSpawned = 0;
					waveReps++;
					// cluster
				}
			}
			else
			{
				numWaves++;
				waveReps = 0;
			}
		}
		else if (numWaves == 5)
		{
			if (waveReps != 3)
			{
				//Loop below thrice then increment wave counter
				// 5 minis
				if ((timer >= 5.5f) && (3 > totalNonClusterSpawned) && (clusterMode == false))
				{
					totalNonClusterSpawned++;
					Instantiate(MiniEnemy, new Vector3(this.transform.position.x, -3, 0), Quaternion.identity);
					Debug.Log("lets go");
					timer = 0.0f;
					//go = enemy.gameObject;
					//go.GetComponent<Rigidbody2D>().gravityScale = 0;
					//go.GetComponent<Rigidbody2D>().velocity = velocity;
				}
				else if ((clusterMode == false) && (3 <= totalNonClusterSpawned))
				{
					//the clustermode changes so that we start spawning the cluster, switch back after cluster is spawned
					clusterMode = true;
				}

				if ((timer >= 5.5f) && (clusterMode == true))
				{
					timer = 0.0f;
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					Instantiate(Slinger, new Vector3(this.transform.position.x - 1, -3, 0), Quaternion.identity);
					Instantiate(BigEnemy, new Vector3(this.transform.position.x, -3, 0), Quaternion.identity);
					Instantiate(BigEnemy, new Vector3(this.transform.position.x + 1, -3, 0), Quaternion.identity);
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					clusterMode = false;
					totalNonClusterSpawned = 0;
					waveReps++;
					// cluster
				}
			}
			else
			{
				numWaves++;
				waveReps = 0;
			}
		}
		else
		{
			if (waveReps != 3)
			{
				//Loop below thrice then increment wave counter
				// 5 minis
				if ((timer >= 5.5f) && (3 > totalNonClusterSpawned) && (clusterMode == false))
				{
					totalNonClusterSpawned++;
					Instantiate(MiniEnemy, new Vector3(this.transform.position.x, -3, 0), Quaternion.identity);
					Debug.Log("lets go");
					timer = 0.0f;
					//go = enemy.gameObject;
					//go.GetComponent<Rigidbody2D>().gravityScale = 0;
					//go.GetComponent<Rigidbody2D>().velocity = velocity;
				}
				else if ((clusterMode == false) && (3 <= totalNonClusterSpawned))
				{
					//the clustermode changes so that we start spawning the cluster, switch back after cluster is spawned
					clusterMode = true;
				}

				if ((timer >= 5.5f) && (clusterMode == true))
				{
					timer = 0.0f;
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					Instantiate(Slinger, new Vector3(this.transform.position.x - 1, -3, 0), Quaternion.identity);
					Instantiate(BigEnemy, new Vector3(this.transform.position.x, -3, 0), Quaternion.identity);
					Instantiate(BigEnemy, new Vector3(this.transform.position.x + 1, -3, 0), Quaternion.identity);
					//stantiate(MiniEnemy, new Vector3(-8, -3, 0), Quaternion.identity);
					clusterMode = false;
					totalNonClusterSpawned = 0;
					waveReps++;
					// cluster
				}
			}
			else
			{
				numWaves++;
				waveReps = 0;
			}
		}
	}
	// spawns an enemy based on the enemy level that you selected
	private void spawnEnemy()
	{
		GameObject Enemy = (GameObject)Instantiate(Enemies[enemyLevel], gameObject.transform.position, Quaternion.identity);
		Enemy.SendMessage("setName", SpawnID);
		// Increase the total number of enemies spawned and the number of spawned enemies
		numEnemy++;
		spawnedEnemy++;
	}
	// Call this function from the enemy when it "dies" to remove an enemy count
	public void killEnemy(int sID)
	{
		// if the enemy's spawnId is equal to this spawnersID then remove an enemy count
		if (SpawnID == sID)
		{
			numEnemy--;
		}
	}
	//enable the spawner based on spawnerID
	public void enableSpawner(int sID)
	{
		if (SpawnID == sID)
		{
			Spawn = true;
		}
	}
	//disable the spawner based on spawnerID
	public void disableSpawner(int sID)
	{
		if (SpawnID == sID)
		{
			Spawn = false;
		}
	}
	// returns the Time Till the Next Wave, for a interface, ect.
	public float TimeTillWave
	{
		get
		{
			return timeTillWave;
		}
	}
	// Enable the spawner, useful for trigger events because you don't know the spawner's ID.
	public void enableTrigger()
	{
		Spawn = true;
	}
}
