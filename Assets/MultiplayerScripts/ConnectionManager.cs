using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour {

	//Function Purpose
	//Check whether both client and host joins a particular game
	public Text testDisplay;

	Player[] players;

	void Update () {
		players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			testDisplay.text = "Adichu mone :D";
		}
	}
}
