using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyCleanupManagerRight : MonoBehaviour {

	public Canvas uicanvas;

	int check = 0;
	public int destroyed = 0;
	public static EnemyCleanupManagerRight Instance;
	void OnTriggerEnter (Collider col) 
	{	

		Instance = this;
		if (col.gameObject.tag == "EnemyFire") {
			Destroy (col.gameObject);
			check = UIController.Instance.state;
			if(check == 2)
			{
				uicanvas.GetComponent<UIController> ().updateScore (1);
			}
		} 

		else if (col.gameObject.tag == "EnemyShip") {
			Debug.Log("No place for ships");
			Destroy (col.gameObject);
			destroyed++;
			Debug.Log("ShipsDIEEED: " + destroyed);
			check = UIController.Instance.state;
			if(check == 2)
			{
				uicanvas.GetComponent<UIController> ().updateScore (5);
			}
		}
	}
}

