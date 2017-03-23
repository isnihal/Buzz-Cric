using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MultiplayerTeamManager : MonoBehaviour {

	Player[] players;
	Player serverPlayer,clientPlayer;
	public Text testDisplay;
	string hostTeam,clientTeam;
	public GameObject teamSelectCanvas,testCanvas;

	void Awake()
	{
		players = FindObjectsOfType<Player> ();
		hostTeam = "NULL";
		clientTeam = "NULL";
	}

	void Update()
	{
		if (players.Length == 2)
		{
			serverPlayer = players [0];//Always player[0] is the server
			clientPlayer = players [1];//Always player[1] is the client
		}

		if (serverPlayer.teamName=="NULL") {
			
			//This code works on Server Only
			if (serverPlayer.isServer) {
				teamSelectCanvas.SetActive (true);
				testCanvas.SetActive (false);
				if (TeamManager.getCurrentTeam () != "NULL") {
					serverPlayer.teamName = TeamManager.getCurrentTeam ();
				}
			}


			//This code works on Client Only
			if (!clientPlayer.isServer) {
				teamSelectCanvas.SetActive (false);
				testCanvas.SetActive (true);
				testDisplay.text = "Waiting";
			}

		} 

		else {
			Debug.Log ("Time to choose client");
		}
	}
		
}
