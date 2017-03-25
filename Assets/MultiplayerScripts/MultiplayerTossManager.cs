using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerTossManager : MonoBehaviour {

	static bool hasTossFinished;
	int tossResult;

	void Start()
	{
		hasTossFinished = false;
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
}
