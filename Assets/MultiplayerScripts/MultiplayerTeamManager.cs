using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;

public class MultiplayerTeamManager : MonoBehaviour {

	Player[] players;
	Player serverPlayer,clientPlayer;
	public Text testDisplay;
	public GameObject testCanvas;

	void Awake()
	{
		players = FindObjectsOfType<Player> ();
	}

	void Update()
	{
		
	}
		
}
