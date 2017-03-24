using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MultiplayerTeamManager : MonoBehaviour {

	Player[] players;

	void Start()
	{
		players = FindObjectsOfType<Player> ();
	}

	void Update()
	{
		if (!string.IsNullOrEmpty (players [0].teamName) && !string.IsNullOrEmpty (players [1].teamName)) {
			SceneManager.LoadScene ("M2_SETTINGS");
		}
	}
}
