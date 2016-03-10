using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	public int state = 0;
	public static UIController Instance;
	public Text scoreText;
	float points = 0;

	public void ChangeState(int forwardOrBackward) {
		Instance = this;
		state += forwardOrBackward;
		if (state < 0)
			state = 0;
	
		Debug.Log (state);

		switch (state) {
			case 1:
				EnableR2SearchingState ();
				break;
			case 2:
				EnableMainGameState ();
				break;
			default:
				EnableDefaultState ();
				break;
		}

	}

	void EnableDefaultState() {
		GameObject[] elements = GameObject.FindGameObjectsWithTag ("MainGameUIElement");
		foreach(GameObject element in elements) {
			if (element.GetComponent<Image> ()) {
				element.GetComponent<Image> ().enabled = false;
			}
			else if(element.GetComponent<Text> ()) {
				element.GetComponent<Text> ().enabled = false;
			}
		}

		Text centralText = GameObject.FindGameObjectWithTag("CentralText").GetComponent<Text>();
		centralText.enabled = true;
		centralText.text = "Searching for Game Board";
	}

	void EnableR2SearchingState() {
		GameObject[] elements = GameObject.FindGameObjectsWithTag ("MainGameUIElement");
		foreach(GameObject element in elements) {
			if (element.GetComponent<Image> ()) {
				element.GetComponent<Image> ().enabled = false;
			}
			else if(element.GetComponent<Text> ()) {
				element.GetComponent<Text> ().enabled = false;
			}
		}

		Text centralText = GameObject.FindGameObjectWithTag("CentralText").GetComponent<Text>();
		centralText.enabled = true;
		centralText.text = "Searching for R2D2";
	}

	void EnableMainGameState() {
		GameObject[] elements = GameObject.FindGameObjectsWithTag ("MainGameUIElement");
		
		foreach(GameObject element in elements) {
			if (element.GetComponent<Image> ()) {
				//element.GetComponent<Image> ().enabled = true;
			}
			else if(element.GetComponent<Text> ()) {
				element.GetComponent<Text> ().enabled = true;
				
			}
		}

		Text centralText = GameObject.FindGameObjectWithTag("CentralText").GetComponent<Text>();
		centralText.enabled = false;
	}

	public void EnableGameOverState() {
		GameObject[] elements = GameObject.FindGameObjectsWithTag ("MainGameUIElement");
		foreach(GameObject element in elements) {
			if (element.GetComponent<Image> ()) {
				element.GetComponent<Image> ().enabled = false;
			}
			else if(element.GetComponent<Text> ()) {
				element.GetComponent<Text> ().enabled = false;
			}
		}
		
		Text centralText = GameObject.FindGameObjectWithTag("CentralText").GetComponent<Text>();
		centralText.enabled = true;
		centralText.text = "Game Over";
		InvokeRepeating("Invisible", 1f, 1f);
			
		
	}

	public void updateScore(int updateAmount) {
		points += updateAmount;
		scoreText.text = "Score: " + points;
	}

	public void ResetartGame() {
		points = 0;
		scoreText.text = "Score: " + points;
		EnableMainGameState ();
	}
public void mkInvisible(){
	GameObject[] r2 = GameObject.FindGameObjectsWithTag ("r2cube");
		foreach(GameObject element in r2) 
			element.GetComponent<Renderer> ().enabled = false;
}
}

	
