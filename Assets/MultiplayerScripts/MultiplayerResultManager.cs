using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MultiplayerResultManager : NetworkBehaviour {

	public  Text winnerText,winnerScore,loserScore;

	void Start()
	{
		Player[] players = FindObjectsOfType<Player> ();
		winnerText.text = players [0].winnerNameString + "WINS";
		winnerScore.text = players [0].winnerScore;
		loserScore.text = players [1].loserScore;
	}

	public void DestoryAllPlayers()
	{
		Player[] players = FindObjectsOfType<Player> ();
		for (int i = 0; i < players.Length; i++) {
			Destroy (players [i].gameObject);
		}

		NetworkManager networkManager = FindObjectsOfType<NetworkManager> ();
		if (networkManager != null) {
			Destroy (networkManager.gameObject);
		}
	}
}
