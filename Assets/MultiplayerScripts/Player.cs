using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	[SyncVar]
	public string teamName;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
		teamName = "NULL";
	}

	[Command]
	public void CmdTeamName(string _teamName)
	{
		teamName = _teamName;
	}
}
