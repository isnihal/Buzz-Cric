using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    
    public void LoadLevel(string levelName)
    {
        Debug.Log("Request to load level " + levelName);
        Application.LoadLevel(levelName);
    }

    public void QuitGame()
    {
        Debug.Log("Request to quit game");
        Application.Quit();
    }
}
