using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MultiplayerGameManager : MonoBehaviour {

	static int numberOfChildrenKilled;

	void Update()
	{
		Player[] player = FindObjectsOfType<Player> ();
		if (player.Length == 0) {
			SceneManager.LoadScene ("M5_RESULT");
		}
	}

	public static void loadResult()
	{
		Player[] player = FindObjectsOfType<Player> ();
		for (int i = 0; i < player.Length; i++) {
			numberOfChildrenKilled = 0;
			foreach (Transform child in player[i].transform) {
				if (numberOfChildrenKilled == 0) {
					numberOfChildrenKilled++;
					Destroy (child.gameObject);
				}
			}
		}
		SceneManager.LoadScene ("M5_RESULT");
	}
}
