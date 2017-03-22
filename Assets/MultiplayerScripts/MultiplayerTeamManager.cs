using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MultiplayerTeamManager : MonoBehaviour {

	Player[] players;
	public Text testDisplay;
	string hostTeam,clientTeam;
	Player serverPlayer,clientPlayer;
	public GameObject teamSelectCanvas,testCanvas;

	void Awake()
	{
		players = FindObjectsOfType<Player> ();
		hostTeam = "NULL";
		clientTeam = "NULL";
	}

	void Update()
	{
		if (players.Length == 2) {

			////////////////////////////
			if (players [0].isServer) {
				
				if (hostTeam == "NULL") {
					if (TeamManager.getCurrentTeam () != "NULL") 
					{
						players [0].teamName = TeamManager.getCurrentTeam ();
						TeamManager.setCurrentTeamNull ();
					}
				}
			} 
			else 
			{
				if (hostTeam != "NULL") 
				{
					testDisplay.text = "Host Team(Y)";
				}
			}
			///////////////////////////
			if (players [1].isServer)
			{
				if (hostTeam == "NULL") {
					if (TeamManager.getCurrentTeam () != "NULL") 
					{
						players [0].teamName = TeamManager.getCurrentTeam ();
						TeamManager.setCurrentTeamNull ();
					}
				}
			} 
			else 
			{
				if (hostTeam == "NULL") {
					teamSelectCanvas.SetActive (true);
					testCanvas.SetActive (false);
					testDisplay.text = "Waiting";
				} else {
					testDisplay.text = "Host Team(Y)";
				}
			}
		}
	}
}
