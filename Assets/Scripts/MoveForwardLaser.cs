using UnityEngine;
using System.Collections;

public class MoveForwardLaser : MonoBehaviour {

	public float speed = 40.0f;
	int check = 0;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		LineRenderer beam;
		check = UIController.Instance.state;
		if(check == 2)
		{
			if(EnemySpawnManager.Instance != null)
				speed = EnemySpawnManager.Instance.wavecount * 25.0f;
			transform.position += transform.forward * speed * Time.deltaTime;
			beam = gameObject.GetComponent<LineRenderer> ();
			beam.SetPosition (0, transform.position);
			beam.SetPosition (1, transform.position + transform.forward * speed);
		}
	}
}