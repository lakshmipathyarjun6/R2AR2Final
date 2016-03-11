using UnityEngine;
using System.Collections;

public class MoveForwardShip : MonoBehaviour {

	public float speed = 5.0f;
	int check = 0;
	// Use this for initialization
	void Start () {

	}
    void Update() {
		check = UIController.Instance.state;
		if(check == 2)
		{
			if (EnemySpawnManager.Instance != null) {
				speed = 25; //  EnemySpawnManager.Instance.wavecount;
			}
			transform.position += transform.forward * speed * Time.deltaTime;
    	}
    }
}