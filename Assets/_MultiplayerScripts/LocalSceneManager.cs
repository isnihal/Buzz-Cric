using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalSceneManager : MonoBehaviour {

	//Function purpose:A local scene manager to load the game after the toss

	static int numberOfChildrenKilled;

	void Start()
	{
		numberOfChildrenKilled = 0;
	}

	public static void loadGame()
	{
		Player[] players = FindObjectsOfType<Player> ();
		for (int i = 0; i < players.Length; i++) {
			//Delete objects in both players like toss canvas which are not needed
			numberOfChildrenKilled = 0;
			foreach (Transform child in players[i].transform) {
				if (numberOfChildrenKilled < 6) {//Different toss canvases are 6 in number
					Destroy (child.gameObject);
					numberOfChildrenKilled++;
				}
			}
		}

		//Start the game
		SceneManager.LoadScene ("M4_GAME");
	}
}
