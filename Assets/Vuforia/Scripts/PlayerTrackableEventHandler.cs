/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;

namespace Vuforia
{
	/// <summary>
	/// A custom handler that implements the ITrackableEventHandler interface.
	/// </summary>
	public class PlayerTrackableEventHandler : MonoBehaviour,
	ITrackableEventHandler
	{
		public Text comments;

		#region PRIVATE_MEMBER_VARIABLES

		private TrackableBehaviour mTrackableBehaviour;
		private bool audioSourceAttached = false;

		#endregion // PRIVATE_MEMBER_VARIABLES



		#region UNTIY_MONOBEHAVIOUR_METHODS

		void Start()
		{
			//gameObject.AddComponent<EnemyAI> ();
			mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			if (mTrackableBehaviour)
			{
				mTrackableBehaviour.RegisterTrackableEventHandler(this);
			}
		}

		#endregion // UNTIY_MONOBEHAVIOUR_METHODS



		#region PUBLIC_METHODS

		/// <summary>
		/// Implementation of the ITrackableEventHandler function called when the
		/// tracking state changes.
		/// </summary>
		public void OnTrackableStateChanged(
			TrackableBehaviour.Status previousStatus,
			TrackableBehaviour.Status newStatus)
		{
			if (newStatus == TrackableBehaviour.Status.DETECTED ||
				newStatus == TrackableBehaviour.Status.TRACKED ||
				newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
			{
				OnTrackingFound();
			}
			else
			{
				OnTrackingLost();
			}
			/*while(newStatus != TrackableBehaviour.Status.DETECTED ||
				newStatus != TrackableBehaviour.Status.TR ACKED || newStatus != TrackableBehaviour.Status.EXTENDED_TRACKED)
			{
				System.Threading.Thread.Sleep(1000);
			}*/
		
		}

		#endregion // PUBLIC_METHODS



		#region PRIVATE_METHODS


		private void OnTrackingFound()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

			// Enable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = true;
			}

			// Enable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = true;
			}

			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
			AudioSource r2Sounds = gameObject.AddComponent<AudioSource> ();
			audioSourceAttached = true;
			r2Sounds.clip = Resources.Load("Audio/WelcomeSound") as AudioClip;
			r2Sounds.Play ();

			GameObject.FindGameObjectWithTag ("MainUI").GetComponent<UIController> ().ChangeState (1);
			if (MainTrackableEventHandler.Instance.MusicSelection == -1)
			comments.text = "Hi! Choose a song!";

		}


		private void OnTrackingLost()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

			// Disable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = false;
			}

			// Disable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = false;
			}

			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

			GameObject.FindGameObjectWithTag ("MainUI").GetComponent<UIController> ().ChangeState (-1);

			if (audioSourceAttached) {
				AudioSource r2sounds = gameObject.GetComponent<AudioSource> ();
				r2sounds.Stop ();
			}
			//Time.timeScale =0;
		}

		#endregion // PRIVATE_METHODS
	}
}
