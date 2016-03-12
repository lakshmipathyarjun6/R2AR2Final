using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyCleanupManager : MonoBehaviour {
	public Canvas uicanvas;

	int check = 0;

	void OnTriggerEnter (Collider col) 
	{	

		if (col.gameObject.tag == "EnemyFire") {
			Destroy (col.gameObject);
			check = UIController.Instance.state;
			if(check == 2)
			{
				uicanvas.GetComponent<UIController> ().updateScore (1);
			}
		} 
			
	}
		
}
