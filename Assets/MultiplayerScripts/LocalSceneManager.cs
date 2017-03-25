using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalSceneManager : MonoBehaviour {

	static int numberOfChildrenKilled;

	void Start()
	{
		numberOfChildrenKilled = 0;
	}

	public static void loadGame()
	{
		Player[] players = FindObjectsOfType<Player> ();
		for (int i = 0; i < players.Length; i++) {
			numberOfChildrenKilled = 0;
			foreach (Transform child in players[i].transform) {
				if (numberOfChildrenKilled < 6) {
					Destroy (child.gameObject);
					numberOfChildrenKilled++;
				}
			}
		}

		SceneManager.LoadScene ("M4_GAME");
	}
}
