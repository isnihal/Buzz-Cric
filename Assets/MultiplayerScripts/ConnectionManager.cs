using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ConnectionManager : NetworkBehaviour {

	//Function Purpose
	//Check whether both client and host joins a particular game
	//Manage the network state of the game
	public Text testDisplay;
	Player[] players;
	static bool hasClientJoined=false;
	string hostTeam,clientTeam;

	void Start()
	{
		hostTeam = "NULL";
		clientTeam = "NULL";
	}

	void Update () {
		if (!hasClientJoined) {
			players = FindObjectsOfType<Player> ();
			if (players.Length == 2) {
				hasClientJoined = true;
				SceneManager.LoadScene ("M1_TEAMS");
			}
		}

		if (SceneManager.GetActiveScene ().buildIndex == 9) {
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
}
