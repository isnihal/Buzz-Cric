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
	bool hasClientJoined=false;


	void Update () {
		if (!hasClientJoined) {
			players = FindObjectsOfType<Player> ();
			if (players.Length == 2) {
				hasClientJoined = true;
				SceneManager.LoadScene ("M1_TEAMS");
			}
		}
	}
}
