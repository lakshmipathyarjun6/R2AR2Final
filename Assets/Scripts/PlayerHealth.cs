using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	float maxHealth = 100.0f;
	float curHealth = 0.0f;
	//public GameObject abuseBar;
	public GameObject smokingEffect;
	public GameObject[] flareEffects;
	public GameObject MainTracker;

	// Use this for initialization
	void Start () {
		curHealth = maxHealth; 
		flareEffects[0].GetComponent<ParticleSystem> ().enableEmission = false;
		flareEffects[1].GetComponent<ParticleSystem> ().enableEmission = false;
		smokingEffect.GetComponent<ParticleSystem> ().enableEmission = false;
	}
	
	// Update is called once per frame
	/*void Update () {
		float abuse_calc = curHealth / maxHealth;
		SetAbuseBar (abuse_calc);
	}*/

	public void TakeAbuse (float amount) {
		curHealth -= amount;

		if (curHealth <= 60.0f) {
			flareEffects[0].GetComponent<ParticleSystem> ().enableEmission = true;
		}

		if (curHealth <= 40.0f) {
			flareEffects[1].GetComponent<ParticleSystem> ().enableEmission = true;
		}

		if (curHealth <= 20.0f) {
			smokingEffect.GetComponent<ParticleSystem> ().enableEmission = true;
		}

		if (curHealth <= 0.0f) {
			AudioSource deathScream = gameObject.GetComponent<AudioSource> ();
			deathScream.clip = Resources.Load("Audio/R2Scream") as AudioClip;
			deathScream.Play ();
			curHealth = 0.0f;

			AudioSource mainAudio = MainTracker.GetComponent<AudioSource> ();
			mainAudio.Stop ();

			mainAudio.clip = MainTracker.GetComponent<Vuforia.MainTrackableEventHandler> ().soundtracks [4];
			mainAudio.Play ();

			GameObject.FindGameObjectWithTag ("MainUI").GetComponent<UIController> ().EnableGameOverState ();

			//GameObject mainBoard = GameObject.FindGameObjectWithTag ("MainBoard");
			//mainBoard.GetComponent<EnemySpawnManager> ().canSpawn = false;

			GameObject mainBoard = GameObject.FindGameObjectWithTag ("MainTarget");
			mainBoard.GetComponent<EnemySpawnManager> ().canSpawn = false;

			MainTracker.GetComponent<CleanupMaster> ().cleanUpAll ();

			GameObject tryAgainCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			tryAgainCube.transform.parent = GameObject.FindGameObjectWithTag ("MainTarget").GetComponent<Transform> ();
			tryAgainCube.transform.localPosition = new Vector3 (0.0f, 0.2f, -0.12f);
			tryAgainCube.transform.localScale = new Vector3 (0.3333333f, 0.04666667f, 0.03333334f);
			tryAgainCube.transform.Rotate (0.0f, 180.0f, 0.0f);
			tryAgainCube.GetComponent<Renderer>().material = Resources.Load("Materials/TryAgain") as Material;
			tryAgainCube.GetComponent<BoxCollider> ().isTrigger = true;
			tryAgainCube.name = "TryAgain";
			tryAgainCube.tag = "MusicSelectionBox";

		}
			
	}

	public void ResetHealth() {
		curHealth = maxHealth;
	}

	/*void SetAbuseBar(float abuse) {
		abuseBar.transform.localScale = new Vector3 (abuse,abuseBar.transform.localScale.y,abuseBar.transform.localScale.z);
	}*/

}
