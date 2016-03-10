using UnityEngine;

using System.Collections;

public class CleanupMaster : MonoBehaviour {
	
	public void cleanUpAll () 
	{
		GameObject[] allVaderShips = GameObject.FindGameObjectsWithTag ("EnemyShip");
		foreach(GameObject ship in allVaderShips) {
			Destroy (ship);
			
			//Text scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
			
			//onGUI(5);
		}

		GameObject[] allFire = GameObject.FindGameObjectsWithTag ("EnemyFire");
		foreach(GameObject fire in allFire) {
			Destroy (fire);
			//onGUI(1);
			//Text scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
			
		}

	}
	
}


