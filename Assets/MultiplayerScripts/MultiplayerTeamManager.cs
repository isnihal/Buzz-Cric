using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MultiplayerTeamManager : MonoBehaviour {

	//A Local scene manager that loads the settings screen after both players select teams

	Player[] players;
	int numberOfChildrenDestroyed;

	void Start()
	{
		players = FindObjectsOfType<Player> ();
		numberOfChildrenDestroyed = 0;
	}

	void Update()
	{
		if (!string.IsNullOrEmpty (players [0].teamName) && !string.IsNullOrEmpty (players [1].teamName)) {
			for (int i = 0; i < players.Length; i++) {
				numberOfChildrenDestroyed = 0;
				foreach (Transform child in players[i].transform) {
					if (numberOfChildrenDestroyed < 3) {
						Destroy (child.gameObject);
						numberOfChildrenDestroyed++;
					}
				}
			}
			SceneManager.LoadScene ("M2_SETTINGS");
		}
	}
}
