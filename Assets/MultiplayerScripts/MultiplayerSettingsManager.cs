using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MultiplayerSettingsManager : NetworkBehaviour {

	Player[] players;

	// Use this for initialization
	void Start () {
		players = FindObjectsOfType<Player> ();
		players [0].settingsCanvas.SetActive (true);
		players [1].settingsCanvas.SetActive (true);
	}
}
