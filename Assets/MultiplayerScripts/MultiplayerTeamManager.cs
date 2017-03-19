using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;

public class MultiplayerTeamManager : NetworkBehaviour {

	//Choose Home and Away teams,Return team names,Return batsmen.

	public Sprite[] teamFlags;//Image array to store team flags
	public Image teamFlagDisplay;//Display board
	public Text teamNameDisplay;//Team name display board
	int flagIndex;
	static int homeTeamIndex;
	static string homeTeam, awayTeam;//Strings to store home and away teams


	Dictionary<int, string> teamName = new Dictionary<int, string>();
	Dictionary<int, string> fullTeamName = new Dictionary<int, string>();


	public GameObject canvasDisable;

	void Start()
	{
		//Setting up of team with respect to flagIndex
		teamName.Add (0, "IND");
		teamName.Add (1, "AUS");
		teamName.Add (2, "ENG");
		teamName.Add (3, "RSA");
		teamName.Add (4, "NZ");
		teamName.Add (5, "PAK");
		teamName.Add (6, "SL");
		teamName.Add (7, "WI");
		teamName.Add (8, "ZIM");
		teamName.Add (9, "IRE");

		//Full names of teams with respect to flagIndex
		fullTeamName.Add (0, "INDIA");
		fullTeamName.Add (1, "AUSTRALIA");
		fullTeamName.Add (2, "ENGLAND");
		fullTeamName.Add (3, "SOUTH AFRICA");
		fullTeamName.Add (4, "NEW ZEALAND");
		fullTeamName.Add (5, "PAKISTAN");
		fullTeamName.Add (6, "SRI LANKA");
		fullTeamName.Add (7, "WEST INDIES");
		fullTeamName.Add (8, "ZIMBAVE");
		fullTeamName.Add (9, "IRELAND");
		flagIndex = 0;

		homeTeam = "NULL";
		awayTeam = "NULL";
	}

	void Update()
	{
		if (isLocalPlayer) {
			canvasDisable.SetActive (true);
		} else {
			canvasDisable.SetActive (false);
		}
	}
}
