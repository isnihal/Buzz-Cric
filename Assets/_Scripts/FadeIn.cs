using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {

    Image fadeInPanel;
    Color color;
    float FadeInTime;

    // Use this for initialization
    void Start () {
        fadeInPanel = GetComponent<Image>();
        color = fadeInPanel.color;
        FadeInTime = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        color.a=color.a-FadeInTime*Time.deltaTime;
        fadeInPanel.color = color;
        if(fadeInPanel.color.a<=0)
        {
            Destroy(gameObject);
        }
	}
}
