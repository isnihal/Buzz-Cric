using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MultiplayerSettingsManager : MonoBehaviour {

	//A function similar to local scene manager,Load the toss and delete unwanted objects in the player

	Player[] players;
	int numberOfChildrenDestroyed;

	void Start()
	{
		players = FindObjectsOfType<Player> ();
	}

	void Update()
	{
		if (players [0].numberOfOvers != 0 && players [1].numberOfOvers != 0) {
			for (int i = 0; i < players.Length; i++) {
				numberOfChildrenDestroyed = 0;
				foreach (Transform child in players[i].transform) {
					if (numberOfChildrenDestroyed < 3) {
						Destroy (child.gameObject);
						numberOfChildrenDestroyed++;
					}
				}
			}
			SceneManager.LoadScene ("M3_TOSS");
		}
	}
}
