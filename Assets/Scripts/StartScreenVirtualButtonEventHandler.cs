using UnityEngine;
using System.Collections;

using Vuforia;

public class StartScreenVirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
	/// <summary>
	/// Called when the scene is loaded
	/// </summary>
	void Start() { 
		VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
		for (int i = 0; i < vbs.Length; ++i) {
			// Register with the virtual buttons TrackableBehaviour
			vbs[i].RegisterEventHandler(this);
		}
	}

	/// <summary>
	/// Called when the virtual button has just been pressed:
	/// </summary>
	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb) { 

		GameObject maintarget = GameObject.FindGameObjectWithTag ("MainTarget");

		switch(vb.VirtualButtonName) {
		case "btnMainTheme":
			maintarget.GetComponent<Vuforia.MainTrackableEventHandler> ().StartGame ("MainThemeMusicOption");
			break;
		case "btnImperialMarch":
			maintarget.GetComponent<Vuforia.MainTrackableEventHandler> ().StartGame ("ImperialMarchMusicOption");
			break;
		case "btnDualOfFates":
			maintarget.GetComponent<Vuforia.MainTrackableEventHandler> ().StartGame ("DuelOfFatesMusicOption");
			break;
		case "btnBattleOfHeroes":
			maintarget.GetComponent<Vuforia.MainTrackableEventHandler> ().StartGame ("BattleOfTheHeroesMusicOption");
			break;
		default:
			throw new UnityException("Button not supported: " + vb.VirtualButtonName);
			break;
		}
	}

	/// <summary>
	/// Called when the virtual button has just been released:
	/// </summary>
	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) { }
}