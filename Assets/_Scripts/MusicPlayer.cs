using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    public AudioClip[] musicArray;
    AudioSource musicPlayer;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
        musicPlayer = gameObject.GetComponent<AudioSource>();
	}

    void OnLevelWasLoaded(int level)
    {
        musicPlayer.clip = musicArray[Application.loadedLevel];
        if (musicPlayer.clip)
        {
            Debug.Log("Playing music " + musicPlayer.clip);
            musicPlayer.loop = true;
            musicPlayer.volume = 1;
            musicPlayer.playOnAwake = true;
            musicPlayer.Play();
        }
        else
        {
            Debug.Log("Error:Clip not found");
        }
    }

    public void setVolume(float volume)
    {
        musicPlayer.volume = volume;
    }
}
