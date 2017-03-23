using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	[SyncVar]
	public string teamName;

	public GameObject teamCanvas;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		if (!isLocalPlayer) {
			teamCanvas.SetActive (false);
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
}
