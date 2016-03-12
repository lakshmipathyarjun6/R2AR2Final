using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArduinoBluetooth : MonoBehaviour {

	public Text comments;
    private bool waitResponse;

	void Start () {
        //bluetoothText = GetComponent<Text>();
        //bluetoothText.text = "";

        // Initialize module and connect to paired Bluetooth device
        BtConnector.moduleName("ArjunBT");
        if (!BtConnector.isBluetoothEnabled())
        {
            BtConnector.askEnableBluetooth();
        }
        else
        {
            BtConnector.connect();
        }
	}

	void Update () {
        // Display current status of Bluetooth module
        //comments.text = BtConnector.readControlData();

        // If already connected
	    if (BtConnector.isConnected())
        {
            // Check for PONG (non-blocking)
            if (BtConnector.available())
            {
                string response = BtConnector.readLine();
                if (response.Length > 0)
                {
                    waitResponse = false;
                    if (response == "Fast Movement")
                    {
						DisplayComment("WOAH too fast!");
                    }
					else if (response == "Odd Movement")
                    {
						DisplayComment("Getting....dizzy");
                    }
					//DisplayComment (response);
                }
            }
        }
	}

	public void DisplayComment(string comment) {
		comments.text = comment;
		Invoke("Delay", 2);
	}

	void Delay ()
	{
		comments.text = "";
	}
}
