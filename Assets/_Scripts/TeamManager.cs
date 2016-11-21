using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour {

    public Sprite[] teamFlags;
    public Image teamFlagDisplay;
    public Text teamNameDisplay;
    int flagIndex;
    static int homeTeamIndex;
    static string homeTeam, awayTeam;

    Dictionary<int, string> teamName = new Dictionary<int, string>();
    Dictionary<int, string> fullTeamName = new Dictionary<int, string>();

    void Start()
    {
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

        //Full names of teams with respect to flagIndex
        fullTeamName.Add(0, "INDIA");
        fullTeamName.Add(1, "AUSTRALIA");
        fullTeamName.Add(2, "ENGLAND");
        fullTeamName.Add(3, "SOUTH AFRICA");
        fullTeamName.Add(4, "NEW ZEALAND");
        fullTeamName.Add(5, "PAKISTAN");
        fullTeamName.Add(6, "SRI LANKA");
        fullTeamName.Add(7, "WEST INDIES");
        fullTeamName.Add(8, "ZIMBAVE");
        fullTeamName.Add(9, "IRELAND");
        flagIndex = 0;

        //To avoid home team same as away team,Level 3 indicates the level 02B_AWAY
        if (Application.loadedLevel == 3)
        {
            if (flagIndex == getHomeTeamIndex())
            {
                flagIndex++;
                Debug.Log(flagIndex);
            }
        }

        chooseTeam();
    }

    void chooseTeam()
    {
            //Display the flag
            teamFlagDisplay.sprite = teamFlags[flagIndex];
            //Display team name
            teamNameDisplay.text = fullTeamName[flagIndex];
    }

    public void chooseHomeTeam()
    {
        //Function responsible for choosing the home team
        homeTeam =teamName[flagIndex];
        homeTeamIndex = flagIndex;
    }

    public void chooseAwayTeam()
    {
        //Function responsible for choosing the away team
        awayTeam = teamName[flagIndex];
    }

    public void increaseFlagIndex()
    {
        flagIndex++;
        //To avoid home team being away team,Level 3 indicates the level 02B_AWAY
        if (Application.loadedLevel == 3)
        {
            if (flagIndex == getHomeTeamIndex())
            {
                flagIndex++;
            }
        }
        //To check flagIndex never goes beyong length of array,If so change index back to zero
        if (flagIndex>teamFlags.Length-1)
            {
                flagIndex = 0;
            //To avoid home team being away team,Level 3 indicates the level 02B_AWAY
            if (Application.loadedLevel == 3)
                {
                if (flagIndex == getHomeTeamIndex())
                {
                      flagIndex++;
                }
                Debug.Log(flagIndex);
            }
        }
        chooseTeam();
    }

    public void decreaseFlagIndex()
    {
        flagIndex--;
        //To avoid home team being away team,Level 3 indicates the level 02B_AWAY
        if (Application.loadedLevel == 3)
        {
            if (flagIndex == getHomeTeamIndex())
            {
                flagIndex--;
            }
        }
        //To check flagIndex never goes less than the length of array,If so change index back to maximum index
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

    public static int getHomeTeamIndex()
    {
        return homeTeamIndex;
    }

    public static string getAwayTeam()
    {
        return awayTeam;
    }

    public static string getBatsman(string team,int number)
    {

        //Edit Teams Here

        //India
        if (team == "IND")
        {
            switch (number)
            {
                case 1: return "Rohit";
                case 2: return "Dhawan";
                case 3: return "Kohli";
                case 4: return "Rahane";
                case 5: return "Raina";
                case 6: return "Dhoni";
                case 7: return "Jadeja";
                case 8: return "Ashwin";
                case 9: return "Shami";
                case 10: return "Umesh";
                case 11: return "Bumrah";
            }
        }

        //Australia
        else if (team == "AUS")
        {
            switch (number)
            {
                case 1: return "Finch";
                case 2: return "Warner";
                case 3: return "Smith";
                case 4: return "Khawaja";
                case 5: return "Bailey";
                case 6: return "Maxwell";
                case 7: return "Wade";
                case 8: return "Faulkner";
                case 9: return "Starc";
                case 10: return "Lyon";
                case 11: return "Hazlewood";
            }
        }

        //England
        else if (team == "ENG")
        {
            switch (number)
            {
                case 1: return "Hales";
                case 2: return "Roy";
                case 3: return "Root";
                case 4: return "Morgan";
                case 5: return "Butler";
                case 6: return "Ali";
                case 7: return "Jordan";
                case 8: return "Willey";
                case 9: return "Rashid";
                case 10: return "Plunket";
                case 11: return "Broad";
            }
        }

        //South Africa
        else if (team == "RSA")
        {
            switch (number)
            {
                case 1: return "Amla";
                case 2: return "de Kock";
                case 3: return "Duplesis";
                case 4: return "Devilliers";
                case 5: return "Roussow";
                case 6: return "Miller";
                case 7: return "Behardien";
                case 8: return "Morris";
                case 9: return "Steyn";
                case 10: return "Morkel";
                case 11: return "Tahir";
            }
        }

        //New Zealand
        else if (team == "NZ")
        {
            switch (number)
            {
                case 1: return "Guptil";
                case 2: return "Latham";
                case 3: return "Williamson";
                case 4: return "Taylor";
                case 5: return "Anderson";
                case 6: return "Ronchi";
                case 7: return "Neesham";
                case 8: return "Santner";
                case 9: return "Boult";
                case 10: return "Southee";
                case 11: return "Wagner";
            }
        }

        //Pakistan
        else if (team == "PAK")
        {
            switch (number)
            {
                case 1: return "Azhar";
                case 2: return "Babar";
                case 3: return "Asad";
                case 4: return "Sharjeel";
                case 5: return "Akmal";
                case 6: return "Sarfaraz";
                case 7: return "Wahab";
                case 8: return "Sohail";
                case 9: return "Yasir";
                case 10: return "Amir";
                case 11: return "Rahat";
            }
        }

        //Sri Lanka
        else if (team == "SL")
        {
            switch (number)
            {
                case 1: return "Kusal";
                case 2: return "Thirimanne";
                case 3: return "Tharanga";
                case 4: return "Chandimal";
                case 5: return "Mathews";
                case 6: return "Shanaka";
                case 7: return "Gunathilaka";
                case 8: return "Randiv";
                case 9: return "Mendis";
                case 10: return "Lakmal";
                case 11: return "Malinga";
            }
        }

        //West Indies
        else if (team == "WI")
        {
            switch (number)
            {
                case 1: return "Gayle";
                case 2: return "Charles";
                case 3: return "Samulels";
                case 4: return "Bravo";
                case 5: return "Pollard";
                case 6: return "Ramdin";
                case 7: return "Sammy";
                case 8: return "Narine";
                case 9: return "Holder";
                case 10: return "Ben";
                case 11: return "Roach";
            }
        }


        //Zimbave
        else if(team=="ZIM")
        {
            switch (number)
            {
                case 1: return "Waller";
                case 2: return "Masakadza";
                case 3: return "Cremer";
                case 4: return "Chibhabha";
                case 5: return "Mufudza";
                case 6: return "Madziva";
                case 7: return "Ervine";
                case 8: return "Sibanda";
                case 9: return "Mutumbami";
                case 10: return "Moor";
                case 11: return "Maruma";
            }
        }


        //Ireland
        else if(team=="IRE")
        {
            switch (number)
            {
                case 1: return "Joyce";
                case 2: return "McBrine";
                case 3: return "Porterfield";
                case 4: return "Stirling";
                case 5: return "Rankin";
                case 6: return "Poynter";
                case 7: return "O'Brien";
                case 8: return "Chase";
                case 9: return "Anderson";
                case 10: return "Wilson";
                case 11: return "Young";
            }
        }
        return null;
    }
}
