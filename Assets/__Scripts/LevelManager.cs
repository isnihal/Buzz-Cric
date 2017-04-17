using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelManager : NetworkBehaviour {
    
    public void LoadLevel(string levelName)
    {
		SceneManager.LoadScene (levelName);
    }


	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (SceneManager.GetActiveScene ().buildIndex != 1 && SceneManager.GetActiveScene ().buildIndex != 9
			    && SceneManager.GetActiveScene ().buildIndex != 15 && SceneManager.GetActiveScene ().buildIndex != 10) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
			} else if (SceneManager.GetActiveScene ().buildIndex == 10) {
				NetworkManager manager = FindObjectOfType<NetworkManager> ();
				Destroy (manager.gameObject);
				SceneManager.LoadScene (1);
			}
		}
	}
}
