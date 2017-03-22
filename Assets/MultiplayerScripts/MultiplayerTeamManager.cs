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
			if (players [0].isServer) {
				serverPlayer = players [0];
				clientPlayer = players [1];
			} else {
				serverPlayer = players [1];
				clientPlayer = players [0];
			}
		}

		if (hostTeam == "NULL") {
			if (serverPlayer.isServer) {
				if (TeamManager.getCurrentTeam () != "NULL") {
					serverPlayer.teamName = TeamManager.getCurrentTeam ();
					hostTeam = TeamManager.getCurrentTeam ();
					TeamManager.setCurrentTeamNull ();
				}	
			} else if (clientPlayer.isClient) {
				teamSelectCanvas.SetActive (false);
				testCanvas.SetActive (true);
				testDisplay.text = "Waiting";
			}
		} 
		else {
		
		}
	}
}
