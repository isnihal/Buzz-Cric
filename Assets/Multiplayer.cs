using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Multiplayer : NetworkBehaviour {

	[SyncVar]
	public int run;

	HostBoard hostBoard;
	ClientBoard clientBoard;
	Multiplayer[] players;
	public GameObject canvasDisable;

	// Use this for initialization
	void Start () {
		hostBoard = FindObjectOfType<HostBoard> ();
		clientBoard = FindObjectOfType<ClientBoard> ();
		if (!isLocalPlayer) {
			canvasDisable.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {		
	}

	[Command]
	public void CmdGetRun(int _runs)
	{
			run = _runs;
	}

	public void chooseRun(int _runs)
	{
		if (!isLocalPlayer) {
			return;
		}
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
}
