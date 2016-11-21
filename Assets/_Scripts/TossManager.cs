using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TossManager : MonoBehaviour {

    int tossResult;
    string playerAction,homeTeam,awayTeam;
    static string firstBatter,secondBatter;
    public Text resultText, battingText, bowlingText, playButton;
    static bool hasTossFinished,hasUserDecided;

    void Start()
    {
        hasTossFinished = false;
        hasUserDecided = false;
        homeTeam = TeamManager.getHomeTeam();
        awayTeam = TeamManager.getAwayTeam(); 
    }

    public void tossCoin(string userChoice)
    {
        if (!hasTossFinished)//Ensures that toss is performed only once
        {
            //0 for heads 1 for tails
            Dictionary<int, string> tossResultInString = new Dictionary<int, string>();
            tossResultInString.Add(0, "HEADS");
            tossResultInString.Add(1, "TAILS");
            tossResult = Random.Range(0, 2);
            Debug.Log(tossResult + "");
            if (tossResultInString[tossResult] == userChoice)
            {
                //User won the toss
                resultText.text = "YOU WON";
                battingText.text = "BATTING";
                bowlingText.text = "BOWLING";
            }
            else
            {
                int randomChoice = Random.Range(0, 2);
                if (randomChoice == 0)
                {
                    //Opponent chooses batting
                    resultText.text = "YOU LOST\n\nVISITORS BATTING";
                    firstBatter = awayTeam;
                    secondBatter = homeTeam;
                }
                else
                {
                    //Opponent chooses bowling
                    resultText.text = "YOU LOST\n\nVISITORS BOWLING";
                    firstBatter = homeTeam;
                    secondBatter = awayTeam;
                }
                playButton.text = "PLAY";
                //Opponent won the toss,so user do not have to decide
                hasUserDecided = true;
            }
            hasTossFinished = true;
        }
    }

    public void chooseAction(string battingOrBowling)
    {
        //This function is called only if user wins the toss
        //User has options to choose batting or bowling
        if (!hasUserDecided)
        {
            playerAction = battingOrBowling;
            if (playerAction == "BATTING")
            {
                resultText.text = "YOU CHOOSE BATTING";
                firstBatter = homeTeam;
                secondBatter = awayTeam;
            }
            else if (playerAction == "BOWLING")
            {
                resultText.text = "YOU CHOOSE BOWLING";
                firstBatter = awayTeam;
                secondBatter = homeTeam;
            }
            playButton.text = "PLAY";
            hasUserDecided = true;
        }
    }

    public static string getFirstBatter()
    {
        return firstBatter;
    }

    public static string getSecondBatter()
    {
        return secondBatter;
    }

    public void startGame()
    {
        if(hasUserDecided && hasTossFinished)
        {
            Application.LoadLevel("02F_GAME");
        }
    }
}
