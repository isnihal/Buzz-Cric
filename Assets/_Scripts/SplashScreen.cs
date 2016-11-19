using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Load Menu function
        loadMenu();
	}

    void loadMenu()
    {
        //Loads Menu after a certain time
        float loadTime = 3f;
        Invoke("autoLoad", loadTime);
    }

    void autoLoad()
    {
        //Load the level/scene 01A_START
        Application.LoadLevel("01A_START");
    }
}
