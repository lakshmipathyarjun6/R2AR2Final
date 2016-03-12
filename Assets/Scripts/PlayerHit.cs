using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHit : MonoBehaviour {

	public PlayerHealth healthLevel;
	public ArduinoBluetooth commentManager;
	public GameObject explosionEffect;

	void OnTriggerEnter (Collider col) {


		
		if (col.gameObject.tag == "EnemyShip") {

			gameObject.GetComponent<AudioSource> ().clip = Resources.Load("Audio/ShipCrash") as AudioClip;

			healthLevel.TakeAbuse (20.0f);

			gameObject.GetComponent<AudioSource> ().Play ();

			Debug.Log ("Ouch what the hell man?!");
			Destroy (col.gameObject);
			EnemyCleanupManager.Instance.destroyed++;
		} 

		else if (col.gameObject.tag == "EnemyFire") {

			gameObject.GetComponent<AudioSource> ().clip = Resources.Load("Audio/PlayerHit") as AudioClip;

			healthLevel.TakeAbuse (10.0f);
			commentManager.DisplayComment("WOAAHHHHHH DONT DO THAT SHIT BRUH!!!!");
			gameObject.GetComponent<AudioSource> ().Play ();
			Instantiate (explosionEffect, transform.position + new Vector3(-2.0f,6.0f,0.0f), transform.rotation);

			Destroy (col.gameObject);
		} 

		else if (col.gameObject.tag == "MusicSelectionBox") {
			GameObject maintarget = GameObject.FindGameObjectWithTag ("MainTarget");
			maintarget.GetComponent<Vuforia.MainTrackableEventHandler> ().StartGame (col.gameObject.name);

			if (col.gameObject.name == "TryAgain") {
				healthLevel.ResetHealth ();
			}
		}
	}
}
