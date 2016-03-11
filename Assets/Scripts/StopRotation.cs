using UnityEngine;
using System.Collections;

using Vuforia;

public class StopRotation : MonoBehaviour
{

	// Update is called once per frame
	void Update()
	{
		gameObject.transform.rotation = Quaternion.identity;
	}
}
