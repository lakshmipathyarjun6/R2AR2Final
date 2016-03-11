using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {

	public Transform spawnPoint;
	public Material lasermaterial;
	public GameObject player;
	int check = 0;
	// Use this for initialization
	void Start () {

	}

	public void Fire () {
		check = UIController.Instance.state;
		if(check == 2){
			LineRenderer beam;
			GameObject laser = new GameObject ();
			laser.transform.position = spawnPoint.position;
			laser.transform.LookAt (player.GetComponent<BoxCollider>().ClosestPointOnBounds(laser.transform.position));
			laser.name = "shipFire";
			laser.tag = "EnemyFire";

			laser.AddComponent<BoxCollider> ();
			BoxCollider collider = laser.GetComponent<Collider>() as BoxCollider;
			collider.center = new Vector3(0.0f,0.0f,12.5f);
			collider.size = new Vector3 (15f, 15f, 25.0f);
			collider.isTrigger = true;

			laser.AddComponent<Rigidbody> ();
			Rigidbody body = laser.GetComponent<Rigidbody> ();
			body.useGravity = false;

			beam = laser.AddComponent<LineRenderer> ();
			beam.material = lasermaterial;
			beam.SetWidth (0.0f, 15f);
			laser.AddComponent<MoveForwardLaser>();

			laser.AddComponent<AudioSource> ();
			AudioSource fire = laser.GetComponent<AudioSource> ();
			fire.clip = Resources.Load("Audio/VaderTieFire") as AudioClip;
			fire.Play ();
		}
	}
} 
