using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MultiplayerGameManager : MonoBehaviour {

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
			Destroy (player [i].gameObject);
		}
		SceneManager.LoadScene ("M5_RESULT");
	}
}
