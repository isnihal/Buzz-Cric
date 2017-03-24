using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	[SyncVar]
	public string teamName;

	[SyncVar]
	public bool hostSelected;

	public GameObject teamCanvas,testCanvas;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		if (isServer) {
			if (isLocalPlayer) {
				showTeamCanvas ();
			}
		} 

		if(!isServer) {
			if (isLocalPlayer) {
				showTestCanvas ();
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
		players [0].hostSelected = true;
		players [1].hostSelected = true;
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
	}
		
	public void showTestCanvas()
	{
		testCanvas.SetActive (true);
		teamCanvas.SetActive (false);
	}

	public void showTeamCanvas()
	{
		teamCanvas.SetActive (true);
		testCanvas.SetActive (false);
	}
}
