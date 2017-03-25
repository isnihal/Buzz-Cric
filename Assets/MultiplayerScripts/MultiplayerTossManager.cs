using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MultiplayerTossManager : NetworkBehaviour{

	static bool hasTossFinished;
	int tossResult;
	Player[] players;
	float timeLeft;
	int numberOfChildrenDestroyed;

	void Start()
	{
		hasTossFinished = false;
		players = FindObjectsOfType<Player> ();
		timeLeft = 5;
		numberOfChildrenDestroyed = 0;
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
			if (tossResultInString[tossResult] == userChoice)
			{
				//Host won the toss
				Player.hostWonTheToss();
			}
			else
			{
				//Client Won the toss
				Player.clientWontTheToss();
			}
			hasTossFinished = true;
		}
	}

	void Update()
	{
		if (players.Length == 2) {
			if (players [0].statusText != null && players [1].statusText != null) {
				if (players [0].timerText != null && players [1].timerText != null) {
					if (players [0].isBatter || players [1].isBatter) {
						if (players [0].isLocalPlayer) {
							players [0].showTossResultCanvas ();
						}

						if (players [1].isLocalPlayer) {
							players [1].showTossResultCanvas ();
						}
						if (players [0].isBatter) {
							players [0].statusText.text = "You are Batting first";
							players [1].statusText.text = "You are Bowling first";
						} else {
							players [1].statusText.text = "You are Batting first";
							players [0].statusText.text = "You are Bowling first";
						}

						timeLeft -= Time.deltaTime;
						players [0].timerText.text = "GAME STARTS IN " + (int)timeLeft + "";
						players [1].timerText.text = "GAME STARTS IN " + (int)timeLeft + "";

						if (timeLeft <= 0) {
							LocalSceneManager.loadGame ();
						}
					}
				}
			}
		}
	}
}
