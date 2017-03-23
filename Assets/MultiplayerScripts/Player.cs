using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	[SyncVar]
	public string teamName;

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
		} else if(!isServer) {
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

	public void chooseTeam()
	{
		if (!isLocalPlayer) {
			return;
		}
		CmdSyncTeamName (TeamManager.getCurrentTeam());
		TeamManager.setCurrentTeamNull ();
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
