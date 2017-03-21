using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour {

	//Function Purpose
	//Check whether both client and host joins a particular game
	//Manage the network state of the game
	public Text testDisplay;
	Player[] players;
	bool hasClientJoined;

	void Start()
	{
		hasClientJoined = false;
	}

	void Update () {
		if (!hasClientJoined) {
			players = FindObjectsOfType<Player> ();
			if (players.Length == 2)
				SceneManager.LoadScene ("TEST");
			}
	}
}
