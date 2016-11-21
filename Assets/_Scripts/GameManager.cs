using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    static int numberOfOvers, numberOfBalls,firstInningRuns,secondInningRuns, wicketsGone, striker, nonStriker, nextBatsman, strikerRuns, nonStrikerRuns,targetRuns,totalOvers,totalBalls;
    int nextBallScore;
    public Text ballDisplay, strikerDisplay, nonStrikerDisplay, overDisplay, runDisplayText,targetText;
    bool firstInnings;

    // Use this for initialization
    void Start () {
        firstInnings = true;
        targetRuns = -1;
        resetGameVariables();

        totalOvers = 20;
        totalBalls = 120;
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
                Debug.Log("Game Over");
                gameOver();
            }
        }
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
        ballDisplay.text = "-";
        striker = 1;
        nonStriker = 2;
        nextBatsman = 3;
    }

    void setScoreBoards()
    {
        overDisplay.text = "OVERS " + numberOfOvers + "." + numberOfBalls;
        if (firstInnings)
        {
            runDisplayText.text = TossManager.getFirstBatter() + " " + firstInningRuns + "/" + wicketsGone;
        }
        else
        {
            runDisplayText.text = TossManager.getSecondBatter() + " " + secondInningRuns + "/" + wicketsGone;
        }
        strikerDisplay.text = "Batsman " + striker + " " + strikerRuns;
        nonStrikerDisplay.text = "Batsman " + nonStriker + " " + nonStrikerRuns;

        //Set runs to win only in second innings
        if(!firstInnings)
        {
            targetText.text = "TO WIN " + (targetRuns-secondInningRuns);
        }
    }


    public void nextBall()
    {
        //Generate runs between 0-6(5 is given as wicket)
        nextBallScore = Random.RandomRange(0, 7);
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
    }
    
    bool InningsBreak()
    {
        if(wicketsGone==10 || numberOfOvers==20)
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
        ballDisplay.text = "W";
        wicketsGone++;
        striker = nextBatsman;
        nextBatsman++;
        strikerRuns = 0;
    }

    void scoreRun()
    {
        //Set main display to runs scored in this ball
        ballDisplay.text = nextBallScore.ToString();
        //Increment strikers runs
        strikerRuns += nextBallScore;
        if(nextBallScore%2==1)
        {
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
