using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnemySpawnManager : MonoBehaviour {

	public GameObject player;
	public GameObject[] enemies;                // The enemy prefabs to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public bool canSpawn = false;
	int count = 1; //to check if wavetext should be displayed
	public int shipCount = 0; // ship count per wave
	public int wavecount = 1; //total wave count
	public float delay1 = 2; //delay between wave text displays
	public float next = 0; // variable I might destroy
	public int shipdead = 0; // to count ships destroyed
	public static EnemySpawnManager Instance;
	// Use this for initialization

	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Delay ()
     {
        Text wavetext = GameObject.FindGameObjectWithTag("WaveText").GetComponent<Text>();
        wavetext.enabled = false;
     }

	public void Init() {
		wavecount = 1;
		count = 1;
		shipCount = 0;
		shipdead = 0;
		canSpawn = true;
	}
		
	void Spawn ()
	{	
		Instance = this;
		if(EnemyCleanupManager.Instance != null)
			shipdead = EnemyCleanupManager.Instance.destroyed;
		Debug.Log("shipdead: " + shipdead);
		if(shipdead >= wavecount*3){
			
			shipCount = 0;
			wavecount++;
			Debug.Log("wavecount: " + wavecount);
			count = 1;
			next = Time.time + delay1;
			EnemyCleanupManager.Instance.destroyed = 0;
		}
		
		if (Time.time > next){
			if( shipCount < wavecount*3 && UIController.Instance.state == 2) {
				if(count == 1  && canSpawn ) {
					Text wavetext = GameObject.FindGameObjectWithTag("WaveText").GetComponent<Text>();
					wavetext.text = "Wave " + wavecount;
					wavetext.enabled = true;
					Invoke("Delay", 4);
					count = 0;
				}	
				else if (canSpawn && UIController.Instance.state == 2)  {

					GameObject enemyShip;
					shipCount ++;
					Debug.Log("shipCount: " + shipCount);
					// Find a random index between zero and one less than the number of spawn points.
					int enemyIndex = Random.Range (0, enemies.Length);
					int spawnPointIndex = Random.Range (0, spawnPoints.Length);

					// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
					enemyShip = Instantiate (enemies [enemyIndex], spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation) as GameObject;
					enemyShip.transform.parent = GameObject.FindGameObjectWithTag ("MainTarget").transform;
					enemyShip.transform.Rotate(0.0f, 90.0f, 0.0f);
					enemyShip.transform.Rotate(0.0f, 0.0f, -90.0f);

					// Change names to be more readable

					if (enemyShip.name == "star-wars-vader-tie-fighter(Clone)") {
						enemyShip.transform.localScale = new Vector3 (0.008f, 0.008f, 0.008f);
						enemyShip.name = "VaderShip";
					}

					else if (enemyShip.name == "sithcraft(Clone)") {
						enemyShip.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
						enemyShip.name = "SithCraft";
					}

					enemyShip.tag = "EnemyShip";

					enemyShip.AddComponent<BoxCollider> ();
					BoxCollider collider = enemyShip.GetComponent<Collider> () as BoxCollider;
					collider.size = new Vector3 (20.0f, 20.0f, 20.0f);

					enemyShip.AddComponent<Rigidbody> ();
					Rigidbody body = enemyShip.GetComponent<Rigidbody> ();
					body.useGravity = false;

					enemyShip.AddComponent<EnemyAI> ();
					EnemyAI ai = enemyShip.GetComponent<EnemyAI> ();
					ai.player = player;
			
				}
			}
		}
	}
}

