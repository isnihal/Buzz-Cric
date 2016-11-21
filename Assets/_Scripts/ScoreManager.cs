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
            winnerName.text = TossManager.getFirstBatter()+" WINS";
        }
        else
        {
            winnerName.text = TossManager.getSecondBatter()+" WINS";
        }
        firstTeamText.text = TossManager.getFirstBatter() + " " + firstInningRuns;
        secondTeamText.text = TossManager.getSecondBatter() + " " + secondInningRuns;
    }
}
