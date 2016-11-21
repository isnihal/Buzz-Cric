using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour {

    int[] overArray = { 5, 10, 15, 20 };
    public Text overDisplay;
    int overIndex;
    static int numberOfOvers;

    // Use this for initialization
    void Start () {
        overIndex = 0;
        overDisplay.text ="OVERS   "+overArray[overIndex];
	}

    public void increaseOvers()
    {
        if(overIndex<overArray.Length-1)
        {
            overIndex++;
        }
        else
        {
            overIndex = 0;
        }
        overDisplay.text = "OVERS   " + overArray[overIndex];
    }

    public void setNumberOfOvers()
    {
        numberOfOvers = overArray[overIndex];
    }

    public static int getNumberOfOvers()
    {
        return numberOfOvers;
    }
}
