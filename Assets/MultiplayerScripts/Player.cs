using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	[SyncVar]
	public string teamName;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}
}
