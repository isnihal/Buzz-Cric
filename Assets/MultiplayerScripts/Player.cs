using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	[SyncVar]
	public string teamName;
	NetworkIdentity networkIdentity;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
		teamName = "NULL";
	}

	[Command]
	public void CmdSyncTeamName(string _teamName)
	{
		teamName = _teamName;
	}
}
