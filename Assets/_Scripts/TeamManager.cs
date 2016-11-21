using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour {

    public Sprite[] teamFlags;
    public Image teamFlagDisplay;
    int flagIndex;
    static string homeTeam, awayTeam;

    Dictionary<int, string> teamName = new Dictionary<int, string>();

    void Start()
    {
        flagIndex = 0;
        chooseTeam();


        //Setting up of team with respect to flagIndex
        teamName.Add(0, "IND");
        teamName.Add(1, "AUS");
        teamName.Add(2, "ENG");
        teamName.Add(3, "RSA");
        teamName.Add(4, "NZ");
        teamName.Add(5, "PAK");
        teamName.Add(6, "SL");
        teamName.Add(7, "WI");
        teamName.Add(8, "ZIM");
        teamName.Add(9, "IRE");
    }

    void chooseTeam()
    {
            teamFlagDisplay.sprite = teamFlags[flagIndex];
    }

    public void chooseHomeTeam()
    {
        homeTeam =teamName[flagIndex];
        Debug.Log("Home team " + homeTeam);
    }

    public void chooseAwayTeam()
    {
        awayTeam =teamName[flagIndex];
        Debug.Log("Away team " + awayTeam);
    }

    public void increaseFlagIndex()
    {
        flagIndex++;
        if(flagIndex>teamFlags.Length-1)
        {
            flagIndex = 0;
        }
        chooseTeam();
    }

    public void decreaseFlagIndex()
    {
        flagIndex--;
        if (flagIndex<0)
        {
            flagIndex = teamFlags.Length-1;
        }
        chooseTeam();
    }

    public static string getHomeTeam()
    {
        return homeTeam;
    }

    public static string getAwayTeam()
    {
        return awayTeam;
    }
}
