using UnityEngine;
using System.Collections;

public class MakeInvisible : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Renderer>().enabled = false;
	}
}
