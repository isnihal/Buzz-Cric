using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour {

	//--------------------------Sync Vars------------------------------
	//A sync var syncs values from server to client
	[SyncVar]
	public string teamName,batterString;

	[SyncVar]
	public int numberOfOvers,run,firstInningRuns,secondInningRuns,currentOver,striker,nonStriker
	,strikerRuns,nonStrikerRuns;

	[SyncVar]
	public float currentBall;

	[SyncVar]
	public bool hostSelected,syncHostWon,syncTossFinished,isBatter,deliverBall,setDisplayOn,isFirstInnings;
	//-----------------------------------------------------------------


	//Display objects to be toggled on and off
	public GameObject teamCanvas,testCanvas,settingsCanvas,settingsWaitCanvas,tossCanvas,tossWaitCanvas
	,tossWonCanvas,tossLostCanvas,tossResultCanvas,gameCanvas,One,Two,Three,Four,Five,Six;

	public Text timerText,statusText,statusBoard;
	//------------------------------------------------------------------


	//Common Board for displaying runs and overs
	RunBoard runBoard;
	OverBoard overBoard;
	//------------------------------------------------------------------

	//Static variables used for local calculations
	static bool hasTossFinished,clientWon,hostWon,doOnlyOnce;
	static int totalBalls,wicketsGone,nextBatsman;
	static string firstBattingTeam,secondBattingTeam;
	//------------------------------------------------------------------


	//Common Displays for Host and client
	HostBoard hostBoard;
	ClientBoard clientBoard;
	StrikerDisplay strikerDisplay;//Striker batsman display
	NonStrikerDisplay nonStrikerDisplay;//Non Striker batsman display

	//Awake function is executed before the scene loads
	void Awake()
	{
		//Player Object is spawned only on M0_Connection,Preserve that object through all scenes after that
		DontDestroyOnLoad (gameObject);
	}

	//Start function is executed at the begining of every scene
	void Start()
	{
		hasTossFinished = false;//Check whether toss has finished
		clientWon = false;//Check whether client won the toss
		hostWon = false;//Check whether host won the toss

		if (teamCanvas != null && testCanvas != null) {//This condition is true when the scene is
			//M1_TEAMS

			//Let host first choose the teams and client wait-----------------
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
			//------------------------------------------------------------------
		}

	}

	//This function is executed every frame
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

			if (hostBoard == null && clientBoard == null) {//This block is executed only once in M4_TEAMS ONLY
				hostBoard = FindObjectOfType<HostBoard> ();
				clientBoard = FindObjectOfType<ClientBoard> ();
				runBoard = FindObjectOfType<RunBoard> ();
				totalBalls = numberOfOvers * 6;
				overBoard = FindObjectOfType<OverBoard> ();
				strikerDisplay = FindObjectOfType<StrikerDisplay> ();
				nonStrikerDisplay = FindObjectOfType<NonStrikerDisplay> ();
				strikerRuns = 0;
				nonStrikerRuns = 0;
				wicketsGone = 0;
				striker = 1;
				nonStriker = 2;
				nextBatsman = 3;
				Player[] players = FindObjectsOfType<Player> ();
				if (players [0].isBatter) {
					firstBattingTeam = players [0].teamName;
					secondBattingTeam = players [1].teamName;
				} else {
					firstBattingTeam = players [1].teamName;
					secondBattingTeam = players [0].teamName;
				}
				CmdSyncIsFirstInnings (true);
			}


			//Ball the ball
			if (!deliverBall) {
				if (!isBatter) {//Bowler balls the first ball
					if (isLocalPlayer) {
						statusBoard.text = "Press Any Button!";
						setActiveButtons ();
					}
				} 

				if (isBatter) {//Batsman react to the ball
					if (isLocalPlayer) {
						statusBoard.text = "Wait for the Bowler";
						setInActiveButtons ();
					}
				}
			} else {

				if (!isBatter) {//Bowler balls the first ball
					if (isLocalPlayer) {
						statusBoard.text = "Wait for the batsman";
						setInActiveButtons ();
					}
				} 

				if (isBatter) {//Batsman react to the ball
					if (isLocalPlayer) {
						statusBoard.text = "Press any button";
						setActiveButtons ();
					}
				}
			}


			if (currentBall - (int)currentBall == 0) //	A ball is delivered after client and host presses a button
			{
				if(doOnlyOnce)
				{
					analyzeBall ();
					doOnlyOnce = false;
				}
				setDisplay ();
			} 
			else {
				setBlankDisplay ();
				doOnlyOnce = true;
			}

			calculateOvers ();
		}
	}

	//------------------------Command Functions----------------------------
	//A command function sync values from client to server



	[Command]
	public void CmdSyncTeamName(string _teamName)
	{
		//Sync the induvidual team names of player
		//No need of local player check since button is active in only one player
		teamName = _teamName;
	}

	[Command]
	public void CmdSetHostSelectedTrue()
	{
		//Sync the bool,to determine whether host selected their team
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].hostSelected = true;
			players [1].hostSelected = true;
		}
	}

	[Command]
	public void CmdSyncNumberOfOvers()
	{
		//Sync the total number of overs across both player
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].numberOfOvers = SettingsManager.getNumberOfOvers ();
			players [1].numberOfOvers = SettingsManager.getNumberOfOvers ();
		}
	}

	[Command]
	public void CmdsyncHostTossResult(bool result)
	{
		//Sync the toss result,whether host won or not
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].syncHostWon = result;
			players [1].syncHostWon = result;
		}
	}

	[Command]
	public void CmdSyncTossFinished()
	{	
		//Sync whether the toss finished or not
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].syncTossFinished = true;
			players [1].syncTossFinished = true;
		}
	}

	[Command]
	public void CmdSetBatter(bool result)
	{
		//Set the first batting team after the toss
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

	[Command]
	public void CmdGetRun(int _runs)
	{
		//Sync the current run scored by the batsman
		run = _runs;
	}

	[Command]
	public void CmdSyncBatterDisplay(string _batterString)
	{
		//Sync a string showing the runs scored by the batsman
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].batterString = _batterString;
			players [1].batterString = _batterString;
		}
	}

	[Command]
	public void CmdSyncCurrentBall()
	{
		//Sync the current ball
		//A ball is delivered after both client(0.5) and host(0.5) delivers(1)
		currentBall+=0.5f;
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].currentBall = currentBall;
			players [1].currentBall = currentBall;
		}
	}

	[Command]
	public void CmdDeliverBall()
	{
		//Sync whether the bowler balled a ball
		Player[] players = FindObjectsOfType<Player> ();
		if (deliverBall)
		{
			players [0].deliverBall = false;
			players [1].deliverBall = false;
		} 
		else 
		{
			players [0].deliverBall = true;
			players [1].deliverBall = true;
		}
	}

	[Command]
	public void CmdSyncIsFirstInnings(bool _status)
	{
		//Sync whether the current innings is first innings or not
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].isFirstInnings = _status;
			players [1].isFirstInnings = _status;
		}
	}

	[Command]
	public void CmdSyncFirstInningRuns(int _runsScored)
	{
		//Sync the first inning runs
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			firstInningRuns += _runsScored;
			players [0].firstInningRuns = firstInningRuns;
			players [1].firstInningRuns = firstInningRuns;
		}	
	}

	[Command]
	public void CmdSyncSecondInningRuns(int _runsScored)
	{
		//Sync the second inning runs
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			secondInningRuns += _runsScored;
			players [0].secondInningRuns = secondInningRuns;
			players [1].secondInningRuns = secondInningRuns;
		}
	}

	[Command]
	public void CmdSetCurrentBallZero()
	{
		///After each over,sync the current ball number to 0 (A over consists of balls from 1-6)
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].currentBall = 0;
			players [1].currentBall = 0;
		}
	}

	[Command]
	public void CmdSyncCurrentOver()
	{
		//Sync the current over
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			currentOver++;
			players [0].currentOver = currentOver;
			players [1].currentOver = currentOver;
		}
	}

	[Command]
	public void CmdSyncStrikerAndNoNStriker(int _index1,int _index2)
	{
		//Sync the striker and non striker
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].striker = _index1;
			players [1].striker = _index1;

			players [0].nonStriker = _index2;
			players [1].nonStriker = _index2;
		}
	}

	[Command]
	public void CmdSyncStrikerRuns ()
	{
		//Sync the striker runs
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			players [0].strikerRuns = strikerRuns;
			players [1].currentOver = strikerRuns;
		}
	}

	//-----------------------------------------------------------------


	//-----------------------Public Functions-------------------------------
	public void chooseTeam()
	{
		//Assign this button to choose the player's team
		CmdSetHostSelectedTrue ();
		if (!isLocalPlayer) {
			return;
		}
		CmdSyncTeamName (TeamManager.getCurrentTeam());//Sync the team name across the network
		TeamManager.setCurrentTeamNull ();//Team manager stores teamName as current team,a temporary variable
		//set that variable to null
	}

	public void chooseRun(int _runs)
	{

		//Assign this to a button,for a player choose his current run

		//Only assign the run to local player
		if (!isLocalPlayer) {
			return;
		}

		//Choose the current run
		switch (_runs) {
		case 1:
			CmdGetRun (1);
			break;
		case 2:
			CmdGetRun (2);
			break;
		case 3:
			CmdGetRun (3);
			break;
		case 4:
			CmdGetRun (4);
			break;
		case 5:
			CmdGetRun (5);
			break;
		case 6:
			CmdGetRun (6);
			break;
		}
	}

	//------------------------Local Functions-----------------------------


	void analyzeBall()
	{
		//Analyze both runs made by players
		Player[] players = FindObjectsOfType<Player> ();
		if (players.Length == 2) {
			if (players [0].run == players [1].run) 
			{
				//A wicket fall if both the runs are same
				wicketFall ();
			} 
			else 
			{
				//Award the run pressed by the batter,to the batter
				if (players [0].isBatter) {
					if (isFirstInnings) {
						CmdSyncFirstInningRuns (players [0].run);
					} else {
						CmdSyncSecondInningRuns (players [0].run);
					}
					strikerRuns += players [0].run;
					CmdSyncStrikerRuns ();
				} 

				else 
				{
					if (isFirstInnings) {
						CmdSyncFirstInningRuns (players [1].run);
					} else {
						CmdSyncSecondInningRuns (players [1].run);
					}
					strikerRuns += players [1].run;
					CmdSyncStrikerRuns ();
				}
			}
		}
	}

	void wicketFall()
	{
		//Simulate a wicket fall
		wicketsGone++;
		striker = nextBatsman;
		//After first wicket nextBatsman index changes from 3(inital) to 4 and so on
		nextBatsman++;
		strikerRuns = 0;//Next batsman replaces the striker who got out
	}


	void setDisplay()
	{
		//Set the scoreboard displays
		runBoard.GetComponent<Text> ().text = batterString;

		overBoard.GetComponent<Text> ().text = "OVER:" + currentOver + "." + ((int)currentBall % 6);

		strikerDisplay.GetComponent<Text> ().text = TeamManager.getBatsman (firstBattingTeam, striker) + " " + strikerRuns + "*";
		nonStrikerDisplay.GetComponent<Text> ().text = TeamManager.getBatsman (firstBattingTeam, nonStriker) + " " + nonStrikerRuns;


		//Show the current run of host
		if (isLocalPlayer) {
			hostBoard.GetComponent<Text> ().text = run + "";
		}

		//Show the current run of client
		if (!isLocalPlayer) {
			clientBoard.GetComponent<Text>().text = run + "";
		}

		//Sync the batter string to be shown on both players
		if (isBatter) {
			if (isFirstInnings) {
				CmdSyncBatterDisplay (teamName + ":" + firstInningRuns +"/"+wicketsGone);
			} else {
				CmdSyncBatterDisplay (teamName + ":" + firstInningRuns +"/"+wicketsGone);
			}
		}
	}

	void setBlankDisplay()
	{
		//Set a blank display
		if (isLocalPlayer) {
			hostBoard.GetComponent<Text>().text = "";
		}

		if (!isLocalPlayer) {
			clientBoard.GetComponent<Text>().text = "";
		}
	}
		
	void calculateOvers()
	{
		//Calculate whether an over is finished
		if (((int)currentBall%6==0) && (int)currentBall!=0)
		{
			//End of one over
			CmdSyncCurrentOver ();//Sync the over number across the network
			CmdSetCurrentBallZero ();//Sync the current ball as zero to the network
			//Strike Rotation
			rotateStrike();
		}
	}
		
	public void rotateStrike()
	{
		//Swap the striker index and striker runs
		int swap = striker;
		striker = nonStriker;
		nonStriker = swap;
//		swap = strikerRuns;
//		strikerRuns = nonStrikerRuns;
//		nonStrikerRuns = swap;
		CmdSyncStrikerAndNoNStriker (striker, nonStriker);
	}


	void chooseBatting()
	{
		CmdSetBatter (true);
	}

	void chooseBowling()
	{
		CmdSetBatter (false);
	}

	public void setActiveButtons()
	{
		//Set the numbers buttons active
		One.SetActive (true);
		Two.SetActive (true);
		Three.SetActive (true);
		Four.SetActive (true);
		Five.SetActive (true);
		Six.SetActive (true);
	}

	public void setInActiveButtons()
	{
		//Set the number buttons inactive
		One.SetActive (false);
		Two.SetActive (false);
		Three.SetActive (false);
		Four.SetActive (false);
		Five.SetActive (false);
		Six.SetActive (false);
	}


	public void showTestCanvas()
	{
		//Show waiting text
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

	//-----------------------Static Funcitons--------------------------
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
	//-----------------------------------------------------------------

}
