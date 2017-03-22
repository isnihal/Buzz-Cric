using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MultiplayerTeamManager : MonoBehaviour {

	Player[] players;
	public Text testDisplay;

	void Start()
	{
		players = FindObjectsOfType<Player> ();	
	}

	void Update()
	{
		if (players[0].isServer) {
			testDisplay.text = "Server";
		} else {
			testDisplay.text = "Client";
		}

		if (players[1].isServer) {
			testDisplay.text = "Server";
		} else {
			testDisplay.text = "Client";
		}
	}
}
