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
	/// 

    public class MainTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
		public Canvas uicanvas;
		public AudioClip [] soundtracks;
		public Text comments;
		public int MusicSelection = -1;
		public static MainTrackableEventHandler Instance;

        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
    
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
		{	Instance = this;
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

			GameObject.FindGameObjectWithTag ("MainUI").GetComponent<UIController> ().ChangeState (1);

			if (MusicSelection == -1) {
			} 

			else {
				//int track = Random.Range (0,soundtracks.Length);

				//gameObject.AddComponent<AudioSource> ();
				AudioSource themeMusic = gameObject.GetComponent<AudioSource> ();
				//themeMusic.clip = soundtracks [track];
				themeMusic.clip = soundtracks [MusicSelection];
				themeMusic.Play ();

				//GameObject mainBoard = GameObject.FindGameObjectWithTag ("MainBoard");
				//mainBoard.GetComponent<EnemySpawnManager> ().canSpawn = true;

				GameObject mainBoard = GameObject.FindGameObjectWithTag ("MainTarget");
				mainBoard.GetComponent<EnemySpawnManager> ().canSpawn = true;
			}
        }

		public void StartGame(string songSelection) {

			switch (songSelection) {
				case "MainThemeMusicOption":
					MusicSelection = 0;
					break;
				case "ImperialMarchMusicOption":
					MusicSelection = 1;
					break;
				case "DuelOfFatesMusicOption":
					MusicSelection = 2;
					break;
				case "BattleOfTheHeroesMusicOption":
					MusicSelection = 3;
					break;
				default:
					uicanvas.GetComponent<UIController> ().ResetartGame ();
					break;
			}

			gameObject.AddComponent<AudioSource> ();
			AudioSource themeMusic = gameObject.GetComponent<AudioSource> ();
			themeMusic.loop = true;
			themeMusic.clip = soundtracks [MusicSelection];
			themeMusic.Play ();

			//GameObject mainBoard = GameObject.FindGameObjectWithTag ("MainBoard");
			//mainBoard.GetComponent<EnemySpawnManager> ().Init();
			GameObject mainBoard = GameObject.FindGameObjectWithTag ("MainTarget");
			mainBoard.GetComponent<EnemySpawnManager> ().Init ();

			GameObject[] musicBoxes = GameObject.FindGameObjectsWithTag ("MusicSelectionBox");
			foreach(GameObject box in musicBoxes) {
				Destroy (box);
			}

			comments.text = "";
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

			if (MusicSelection == -1) {
			} 

			else {
				AudioSource themeMusic = gameObject.GetComponent<AudioSource> ();
				themeMusic.Stop ();

				//GameObject mainBoard = GameObject.FindGameObjectWithTag ("MainBoard");
				//mainBoard.GetComponent<EnemySpawnManager> ().canSpawn = false;
				GameObject mainBoard = GameObject.FindGameObjectWithTag ("MainTarget");
				mainBoard.GetComponent<EnemySpawnManager> ().canSpawn = false;

				//gameObject.GetComponent<CleanupMaster> ().cleanUpAll ();
			}
        }

        #endregion // PRIVATE_METHODS
    }
}
