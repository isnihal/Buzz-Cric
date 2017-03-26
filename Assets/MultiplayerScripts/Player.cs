using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour {
	[SyncVar]
	public string teamName;

	[SyncVar]
	public int numberOfOvers;

	[SyncVar]
	public bool hostSelected,syncHostWon,syncTossFinished,isBatter;


	public GameObject teamCanvas,testCanvas,settingsCanvas,settingsWaitCanvas,tossCanvas,tossWaitCanvas,tossWonCanvas,tossLostCanvas;
	public GameObject tossResultCanvas,gameCanvas;
	public Text timerText,statusText;

	static bool hasTossFinished,clientWon,hostWon;

	//bool doOnlyOnce;


	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		hasTossFinished = false;
		clientWon = false;
		hostWon = false;

		if (teamCanvas != null && testCanvas != null) {//Multiplayer team manager
			if (isServer) {
				if (isLocalPlayer) {
					showTeamCanvas ();
				}
			} 

			if (!isServer) {
				if (isLocalPlayer) {
					showTestCanvas ();
				}
			}
		} 
	}

	[Command]
	public void CmdSyncTeamName(string _teamName)
	{
		teamName = _teamName;
	}

	[Command]
	public void CmdSetHostSelectedTrue()
	{
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].hostSelected = true;
			players [1].hostSelected = true;
		}
	}

	[Command]
	public void CmdSyncNumberOfOvers()
	{
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].numberOfOvers = SettingsManager.getNumberOfOvers ();
			players [1].numberOfOvers = SettingsManager.getNumberOfOvers ();
		}
	}

	[Command]
	public void CmdsyncHostTossResult(bool result)
	{
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].syncHostWon = result;
			players [1].syncHostWon = result;
		}
	}

	[Command]
	public void CmdSyncTossFinished()
	{	
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].syncTossFinished = true;
			players [1].syncTossFinished = true;
		}
	}

	[Command]
	public void CmdSetBatter(bool result)
	{
		if (result) {
			isBatter = true;
		} else {
			//Wierd logic,But works this way only :P
			Player[] player = FindObjectsOfType<Player> ();
			if (!player [0].isLocalPlayer) {
				player [1].isBatter = true;
			} else {
				player [0].isBatter = true;
			}
		}
	}

	public void chooseTeam()
	{
		CmdSetHostSelectedTrue ();
		if (!isLocalPlayer) {
			return;
		}
		CmdSyncTeamName (TeamManager.getCurrentTeam());
		TeamManager.setCurrentTeamNull ();
	}

	void Update()
	{
		if (hostSelected) {
			if (isServer) {
				if (isLocalPlayer) {
					showTestCanvas ();
				}
			} 

			if(!isServer) {
				if (isLocalPlayer) {
					showTeamCanvas ();
				}
			}
		}
			

		if (teamCanvas == null && testCanvas == null) {
			if (settingsCanvas != null && settingsWaitCanvas != null) {//Multiplayer settings

				if (isServer) {
					if (isLocalPlayer) {
						showSettingsCanvas ();
					}
				} 

				if (!isServer) {
					if (isLocalPlayer) {
						showSettingsWaitCanvas ();
					}
				}
			} else if (tossCanvas != null && testCanvas == null && !syncTossFinished) {//Multiplayer Toss
				
				if (isServer) {
					if (isLocalPlayer) {
						showTossCanvas ();
					}
				} 

				if (!isServer) {
					if (isLocalPlayer) {
						showTossWaitCanvas ();
					}
				}
			} else if (hasTossFinished || syncTossFinished) {
				if (hostWon || syncHostWon) {
					CmdsyncHostTossResult (true);
					if (isServer) {
						if (isLocalPlayer) {
							showTossWonCanvas();
							disableTossCanvas ();
						}
					} 

					if (!isServer) {
						if (isLocalPlayer) {
							showTossLostCanvas();
							disableTossCanvas ();
						}
					}
				} else if (clientWon || !syncHostWon) {
					CmdsyncHostTossResult (false);
					if (isServer) {
						if (isLocalPlayer) {
							showTossLostCanvas();
							disableTossCanvas();
						}
					} 

					if (!isServer) {
						if (isLocalPlayer) {
							showTossWonCanvas();
							disableTossCanvas();
						}
					}
				}
			}
		}

		//Game Logic starts from here
		if (tossResultCanvas == null) {//Toss result canvas is destroyed entering into game scene
			if (isLocalPlayer) {
				gameCanvas.SetActive (true);//Set the same game canvas for both host and client
			}
		}
	}
		
	public void showTestCanvas()
	{
		if (teamCanvas != null && testCanvas != null) {
			testCanvas.SetActive (true);
			teamCanvas.SetActive (false);
		}
	}

	public void showTeamCanvas()
	{
		if (teamCanvas != null && testCanvas != null) {
			teamCanvas.SetActive (true);
			testCanvas.SetActive (false);
		}
	}

	public void showSettingsCanvas()
	{
		if (settingsCanvas != null && settingsWaitCanvas != null) {
			settingsCanvas.SetActive (true);
			settingsWaitCanvas.SetActive (false);
		}
	}

	public void showSettingsWaitCanvas()
	{
		if (settingsCanvas != null && settingsWaitCanvas != null) {
			settingsCanvas.SetActive (false);
			settingsWaitCanvas.SetActive (true);
		}
	}

	public void showTossCanvas()
	{
		if (tossCanvas != null && tossWaitCanvas != null) {
			tossCanvas.SetActive (true);
			tossWaitCanvas.SetActive (false);
		}
	}

	public void showTossWaitCanvas()
	{
		if (tossCanvas != null && tossWaitCanvas != null) {
			tossCanvas.SetActive (false);
			tossWaitCanvas.SetActive (true);
		}
	}

	public void disableTossCanvas()
	{
		if (tossCanvas != null && tossWaitCanvas != null) {
			tossCanvas.SetActive (false);
			tossWaitCanvas.SetActive (false);
		}
	}

	public void showTossWonCanvas()
	{
		if (tossWonCanvas != null && tossLostCanvas != null) {
			tossWonCanvas.SetActive (true);
			tossLostCanvas.SetActive (false);
		}
	}

	public void showTossLostCanvas()
	{
		if (tossWonCanvas != null && tossLostCanvas != null) {
			tossWonCanvas.SetActive (false);
			tossLostCanvas.SetActive (true);
		}
	}

	public void showTossResultCanvas()
	{
		if (tossWonCanvas != null && tossLostCanvas != null && tossResultCanvas != null) {
			tossResultCanvas.SetActive (true);
			tossWonCanvas.SetActive (false);
			tossLostCanvas.SetActive (false);
		}
	}

	public static void hostWonTheToss()
	{
		hasTossFinished = true;
		hostWon = true;
		clientWon = false;
	}

	public static void clientWontTheToss()
	{
		hasTossFinished = true;
		hostWon = false;
		clientWon = true;
	}

	public void chooseBatting()
	{
		CmdSetBatter (true);
	}

	public void chooseBowling()
	{
		CmdSetBatter (false);
	}
		
}
