using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    static int numberOfOvers, numberOfBalls,firstInningRuns,secondInningRuns, wicketsGone, striker, nonStriker, nextBatsman, strikerRuns,                      nonStrikerRuns,targetRuns,totalOvers,totalBalls,nextBallScore;
    public Text strikerDisplay, nonStrikerDisplay, overDisplay, runDisplayText,targetText;
    static bool firstInnings,hasRotationFinished;
    string firstBattingTeam, secondBattingTeam;

    // Use this for initialization
    void Start () {
        firstInnings = true;
        hasRotationFinished = false;
        //No target to chase in firstInnings,-1 because 0 will cause bug
        targetRuns = -1;
        //Variables like numberOfOvers,runs etc are set to zero at the begining
        resetGameVariables();

        //Overs as per the will of the user
        totalOvers = SettingsManager.getNumberOfOvers();
        totalBalls = totalOvers*6;

        //Get names of first and second batting teams from TossManager
        firstBattingTeam = TossManager.getFirstBatter();
        secondBattingTeam = TossManager.getSecondBatter();
	}


    // Update is called once per frame
    void Update()
    {
        setScoreBoards();

        //Check for 1st innings break(All out or 20 overs)
        if (firstInnings)
        {
            if (InningsBreak())
            {
                firstInnings = false;
                targetRuns = firstInningRuns + 1;
                resetGameVariables();
            }
        }

        //Check for 2nd innings break
        if (!firstInnings)
        {
            if (InningsBreak())
            {
                gameOver();
            }
        }

        //Works only if rotation of wheel finishes
        nextBall();
    }

    void resetGameVariables()
    {
        numberOfOvers = 0;
        numberOfBalls = 0;
        if(firstInnings)
        {
            firstInningRuns = 0;
        }
        else
        {
            secondInningRuns = 0;
        }
        strikerRuns = 0;
        nonStrikerRuns = 0;
        wicketsGone = 0;
        striker = 1;
        nonStriker = 2;
        nextBatsman = 3;
    }

    //Set the various Text Display
    void setScoreBoards()
    {
        overDisplay.text = "OVERS " + numberOfOvers + "." + numberOfBalls;
        if (firstInnings)
        {
            runDisplayText.text = firstBattingTeam + " " + firstInningRuns + "/" + wicketsGone;
            strikerDisplay.text = TeamManager.getBatsman(firstBattingTeam,striker) + " " + strikerRuns+"*";
            nonStrikerDisplay.text = TeamManager.getBatsman(firstBattingTeam, nonStriker) + " " + nonStrikerRuns;
        }
        else
        {
            runDisplayText.text = secondBattingTeam + " " + secondInningRuns + "/" + wicketsGone;
            strikerDisplay.text = TeamManager.getBatsman(secondBattingTeam, striker) + " " + strikerRuns+"*";
            nonStrikerDisplay.text = TeamManager.getBatsman(secondBattingTeam, nonStriker)+" " + nonStrikerRuns;
        }

        //Set runs to win only in second innings
        if(!firstInnings)
        {
            targetText.text = "TO WIN " + (targetRuns-secondInningRuns);
        }
    }

   
    public void nextBall()
    {
        //Ensure that next ball is delivered only after rotation of wheel stops
        if (!hasRotationFinished)
            return;

        if (nextBallScore != 5)
        {
            scoreRun();
        }
        else
        {
            wicketFall();
        }
        numberOfBalls++;

        //Check for an over
        calculateOvers();

        //Reset rotation of wheel condition for next ball
        hasRotationFinished = false;
    }

    public static void setNextBallScore(int runs)
    {
        nextBallScore = runs;
        hasRotationFinished = true;
    }

    bool InningsBreak()
    {
        if(wicketsGone==10 || numberOfOvers==totalOvers)
        {
            return true;
        }

        //Check only in second innings,if target is chased
        if(!firstInnings)
        {
            if(secondInningRuns>=targetRuns)
            {
                return true;
            }
        }
        return false;
    }

    void calculateOvers()
    {
        if (numberOfBalls == 6)
        {
            //End of one over
            numberOfOvers++;
            numberOfBalls = 0;
            //Strike Rotation
            int swap = striker;
            striker = nonStriker;
            nonStriker = swap;
            swap = strikerRuns;
            strikerRuns = nonStrikerRuns;
            nonStrikerRuns = swap;
        }
    }

    void wicketFall()
    {
        wicketsGone++;
        striker = nextBatsman;
        //After first wicket nextBatsman index changes from 3(inital) to 4 and so on
        nextBatsman++;
        strikerRuns = 0;
    }

     void scoreRun()
    {
        //Increment strikers runs
        strikerRuns += nextBallScore;
        if(nextBallScore%2==1)
        {
            //Strike rotation if run scored is a 1 or 3
            int swap = striker;
            striker = nonStriker;
            nonStriker = swap;
            swap = strikerRuns;
            strikerRuns = nonStrikerRuns;
            nonStrikerRuns = swap;
        }
        //Increment total score by the runs scored in this ball
        if (firstInnings)
        {
            firstInningRuns += nextBallScore;
        }
        else
        {
            secondInningRuns += nextBallScore;
        }
    }

    //Public static so that score manager can acces this function
    public static int getFirstInningRuns()
    {
        return firstInningRuns;
    }

    public static int getSecondInningRuns()
    {
        return secondInningRuns;
    }

    void gameOver()
    {
        Application.LoadLevel("03A_WIN");
    }



}
