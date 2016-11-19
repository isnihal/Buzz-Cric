using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    static int numberOfOvers, numberOfBalls,firstInningRuns,secondInningRuns, wicketsGone, striker, nonStriker, nextBatsman, strikerRuns, nonStrikerRuns;
    int nextBallScore;
    public Text ballDisplay, strikerDisplay, nonStrikerDisplay, overDisplay, runDisplayText;
    bool firstInnings;

    // Use this for initialization
    void Start () {
        firstInnings = true;
        resetGameVariables();
	}


    // Update is called once per frame
    void Update()
    {
        setScoreBoards();

        //Check for 1st innings break(All out or 20 overs)
        if (InningsBreak())
        {
            firstInnings = false;
            resetGameVariables();
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
            runDisplayText.text = "IND " + firstInningRuns + "/" + wicketsGone;
        }
        else
        {
            runDisplayText.text = "PAK " + secondInningRuns + "/" + wicketsGone;
        }
        strikerDisplay.text = "Batsman" + striker + " " + strikerRuns;
        nonStrikerDisplay.text = "Batsman" + nonStriker + " " + nonStrikerRuns;
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
        Debug.Log("Wickets gone " + wicketsGone);
        Debug.Log("Number of overs " + numberOfOvers);

        //Check for an over
        calculateOvers();
    }
    
    bool InningsBreak()
    {
        if(wicketsGone==10 || numberOfOvers==20)
        {
            return true;
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
