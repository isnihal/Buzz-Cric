#if ENABLE_UNET

namespace UnityEngine.Networking
{
	[AddComponentMenu("Network/NetworkManagerHUD")]
	[RequireComponent(typeof(NetworkManager))]
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class NetworkManagerHUD : MonoBehaviour
	{
		public NetworkManager manager;
		[SerializeField] public bool showGUI = true;
		[SerializeField] public int offsetX;
		[SerializeField] public int offsetY;
		[SerializeField] public GameObject quitButton,matchMakerButton,createInternetMatchButton,
		findInternetMatchButton,stopMatchMakerButton;

		// Runtime variable
		bool showServer = false;

		void Awake()
		{
			manager = GetComponent<NetworkManager>();
		}

		void Update()
		{
			if (!showGUI)
				return;

			if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
			{
				if (Input.GetKeyDown(KeyCode.S))
				{
					manager.StartServer();
				}
				if (Input.GetKeyDown(KeyCode.H))
				{
					manager.StartHost();
				}
				if (Input.GetKeyDown(KeyCode.C))
				{
					manager.StartClient();
				}
			}
			if (NetworkServer.active && NetworkClient.active)
			{
				if (Input.GetKeyDown(KeyCode.X))
				{
					manager.StopHost();
				}
			}
		}

		void OnGUI()
		{
			if (!showGUI)
				return;

			int xpos = 10 + offsetX;
			int ypos = 40 + offsetY;
			int spacing = 24;

			if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
			{
				if (/*GUI.Button(new Rect(xpos, ypos, 200, 20), "LAN Host(H)")*/false)
				{
					manager.StartHost();
				}
				ypos += spacing;

				if (/*GUI.Button(new Rect(xpos, ypos, 105, 20), "LAN Client(C)")*/false)
				{
					manager.StartClient();
				}
				//manager.networkAddress = GUI.TextField(new Rect(xpos + 100, ypos, 95, 20), manager.networkAddress);
				ypos += spacing;

				if (/*GUI.Button(new Rect(xpos, ypos, 200, 20), "LAN Server Only(S)")*/false)
				{
					manager.StartServer();
				}
				ypos += spacing;
			}
			else
			{
				if (NetworkServer.active)
				{
					//GUI.Label(new Rect(xpos, ypos, 300, 20), "Server: port=" + manager.networkPort);
					//ypos += spacing;
				}
				if (NetworkClient.active)
				{
					//GUI.Label(new Rect(xpos, ypos, 300, 20), "Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
					//ypos += spacing;
				}
			}

			if (NetworkClient.active && !ClientScene.ready)
			{
				if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Client Ready"))
				{
					ClientScene.Ready(manager.client.connection);

					if (ClientScene.localPlayers.Count == 0)
					{
						ClientScene.AddPlayer(0);
					}
				}
				ypos += spacing;
			}

			if (NetworkServer.active || NetworkClient.active)
			{
				//QUIT BUTTON BLOCK
				quitButton.SetActive (true);
				matchMakerButton.SetActive (false);
				createInternetMatchButton.SetActive (false);
				findInternetMatchButton.SetActive(false);
				stopMatchMakerButton.SetActive (false);
			}

			if (!NetworkServer.active && !NetworkClient.active)
			{
				ypos += 10;

				if (manager.matchMaker == null)
				{
					//Match Maker Block
					matchMakerButton.SetActive (true);
					quitButton.SetActive (false);
					createInternetMatchButton.SetActive (false);
					findInternetMatchButton.SetActive(false);
					stopMatchMakerButton.SetActive (false);
				}
				else
				{
					if (manager.matchInfo == null)
					{
						if (manager.matches == null)
						{
							//Create InternetNet match block
							createInternetMatchButton.SetActive(true);
							findInternetMatchButton.SetActive(true);
							matchMakerButton.SetActive (false);
							quitButton.SetActive (false);


							//Room name block

							manager.matchName = "Buzz Cric";

							//Find InternetMatchBlock
							/*findInternetMatchButton.SetActive(true);
							createInternetMatchButton.SetActive(true);
							matchMakerButton.SetActive (false);
							quitButton.SetActive (false);*/

						}
						else
						{
							foreach (var match in manager.matches)
							{
								if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Join Match:" + match.name))
								{
									manager.matchName = match.name;
									manager.matchSize = (uint)match.currentSize;
									manager.matchMaker.JoinMatch(match.networkId, "", manager.OnMatchJoined);
								}
								ypos += spacing;
							}
						}
					}

					if (/*GUI.Button(new Rect(xpos, ypos, 200, 20), "Change MM server")*/false)
					{
						showServer = !showServer;
					}
					if (showServer)
					{
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Local"))
						{
							manager.SetMatchHost("localhost", 1337, false);
							showServer = false;
						}
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Internet"))
						{
							manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
							showServer = false;
						}
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Staging"))
						{
							manager.SetMatchHost("staging-mm.unet.unity3d.com", 443, true);
							showServer = false;
						}
					}

					//Stop MatchMaker
					stopMatchMakerButton.SetActive(true);
				}
			}
		}


		//Implement QuitButton Functionality
		public void quitButtonFunction()
		{
			manager.StopHost ();
		}

		public void matchMakerFunction()
		{
			manager.StartMatchMaker();
		}

		public void createInternetMatchFunction()
		{
			manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", manager.OnMatchCreate);
		}

		public void findInternetMatchFunction()
		{
			manager.matchMaker.ListMatches(0,20, "", manager.OnMatchList);
		}

		public void stopMatchMakerFunction()
		{
			manager.StopMatchMaker();
		}
	}
};
#endif //ENABLE_UNET
