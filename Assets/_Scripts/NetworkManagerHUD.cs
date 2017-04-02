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
		GUIStyle guiStyle;

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
		}

		void OnGUI()
		{
			GUIStyle guiStyle = new GUIStyle(GUI.skin.button);
			GUIStyle textField = new GUIStyle (GUI.skin.textField);
			textField.fontSize = 50;
			guiStyle.fontSize = 50;

			// Load and set Font
			Font myFont = (Font)Resources.Load("Fonts/comic", typeof(Font));
			guiStyle.font = myFont;
			if (!showGUI)
				return;

			int xpos = (Screen.width / 2)-400;
			int ypos = (Screen.height / 2)-350;
			int spacing = 110;


			if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
			{
			}
			else
			{
			}

			if (NetworkClient.active && !ClientScene.ready)
			{
				if (GUI.Button(new Rect(xpos, ypos, 800, 100), "Client Ready",guiStyle))
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
				if (GUI.Button(new Rect(xpos+200,40, 400, 100), "QUIT",guiStyle))
				{
					manager.StopHost();
				}
				ypos += spacing;
			}

			if (!NetworkServer.active && !NetworkClient.active)
			{
				ypos += 100;

				if (manager.matchMaker == null)
				{
					if (GUI.Button(new Rect(xpos, ypos, 800, 100), "Enable Match Maker",guiStyle))
					{
						manager.StartMatchMaker();
					}
					ypos += spacing;
				}
				else
				{
					if (manager.matchInfo == null)
					{
						if (manager.matches == null)
						{
							if (GUI.Button(new Rect(xpos, ypos, 800, 100), "Create Internet Match",guiStyle))
							{
								manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", manager.OnMatchCreate);
							}
							ypos += spacing;

							GUI.Label(new Rect(xpos, ypos, 800, 100), "Room Name:",guiStyle);
							manager.matchName = GUI.TextField(new Rect(xpos, ypos+100, 800, 100), manager.matchName,textField);
							ypos += spacing;

							ypos += 100;

							if (GUI.Button(new Rect(xpos, ypos, 800, 100), "Find Internet Match",guiStyle))
							{
								manager.matchMaker.ListMatches(0,20, "", manager.OnMatchList);
							}
							ypos += spacing;
						}
						else
						{
							foreach (var match in manager.matches)
							{
								if (GUI.Button(new Rect(xpos, ypos, 800, 100), "Join Match:" + match.name,guiStyle))
								{
									manager.matchName = match.name;
									manager.matchSize = (uint)match.currentSize;
									manager.matchMaker.JoinMatch(match.networkId, "", manager.OnMatchJoined);
								}
								ypos += spacing;
							}
						}
					}

					if (GUI.Button(new Rect(xpos, ypos, 800, 100), "Disable Match Maker",guiStyle))
					{
						manager.StopMatchMaker();
					}
					ypos += spacing;
				}
			}
		}
	}
};
#endif //ENABLE_UNET