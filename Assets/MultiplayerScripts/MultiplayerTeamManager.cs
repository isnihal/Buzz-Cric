using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MultiplayerTeamManager : MonoBehaviour {

	Player[] players;
	public Text testDisplay;
	string hostTeam,clientTeam;
	Player serverPlayer,clientPlayer;
	public Canvas teamSelectCanvas,testCanvas;

	void Awake()
	{
		players = FindObjectsOfType<Player> ();
		hostTeam = "NULL";
		clientTeam = "NULL";
	}

	void Update()
	{
		if (players.Length == 2) {
			if (players [0].isServer) {
				if (hostTeam == "NULL") {
				}
			}

			if (!players [1].isServer) {
				if (hostTeam == "NULL") {
					teamSelectCanvas.enabled = false;
				}
			}
		}
	}
}
