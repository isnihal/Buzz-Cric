using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MultiplayerTeamManager : MonoBehaviour {

	Player[] players;
	Player serverPlayer,clientPlayer;
	public Text testDisplay;
	public GameObject teamSelectCanvas,testCanvas;

	void Awake()
	{
		players = FindObjectsOfType<Player> ();
	}

	void Update()
	{
		if (players.Length == 2)
		{
			serverPlayer = players [0];
			clientPlayer = players [1];
		}

		if (serverPlayer.teamName=="NULL") {
			
			//This code works on Server Only
			if (serverPlayer.isServer) {
				teamSelectCanvas.SetActive (true);
				testCanvas.SetActive (false);
				if (TeamManager.getCurrentTeam () != "NULL") {
					serverPlayer.teamName = TeamManager.getCurrentTeam ();
					TeamManager.setCurrentTeamNull ();
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

			//This code works on server only
			if (serverPlayer.isServer) {
				teamSelectCanvas.SetActive (false);
				testCanvas.SetActive (true);
				testDisplay.text = "Waiting";
			}

			//This code works on client only
			if (!clientPlayer.isServer) {
				teamSelectCanvas.SetActive (true);
				testCanvas.SetActive (false);
				if (TeamManager.getCurrentTeam () != "NULL") {
					clientPlayer.teamName = TeamManager.getCurrentTeam ();
					TeamManager.setCurrentTeamNull ();
				}
			}
		}
	}
		
}
