using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour { 
    public Text winnerName,firstTeamText,secondTeamText;
    int firstInningRuns, secondInningRuns;

    // Use this for initialization
    void Start()
    {
        firstInningRuns = GameManager.getFirstInningRuns();
        secondInningRuns = GameManager.getSecondInningRuns();
        if(firstInningRuns>secondInningRuns)
        {
            winnerName.text = "IND WINS";
        }
        else
        {
            winnerName.text = "PAK WINS";
        }
        firstTeamText.text = "IND " + firstInningRuns;
        secondTeamText.text = "PAK " + secondInningRuns;
    }
}
