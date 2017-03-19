using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ConnectionManager : MonoBehaviour {

	//Function Purpose
	//Check whether both client and host joins a particular game

	Player[] players;

	void Update () {
		players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			SceneManager.LoadScene ("M1_TEAMS");
		}
	}
}
