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
			/*OnePlus X ref
			 * Width:1080-800
			 * Height:1920-100
			 * Font size:50
			 * Spacing:1110
			*/

			float screenHeight = Screen.height;
			float screenWidth = Screen.width;
			GUIStyle guiStyle = new GUIStyle(GUI.skin.button);
			GUIStyle textField = new GUIStyle (GUI.skin.textField);
			textField.fontSize = (int)screenHeight/38;
			guiStyle.fontSize = (int)screenHeight/38;

			// Load and set Font
			Font myFont = (Font)Resources.Load("Fonts/comic", typeof(Font));
			guiStyle.font = myFont;
			if (!showGUI)
				return;


			int spacing = (int)screenWidth / 12;
			float width=(float)(screenWidth/1.35);
			float height=(float)(screenHeight/19.2);
			float xpos = (screenWidth / 2)-(width/2);
			float ypos = (screenHeight / 2)-(height/2);


			if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
			{
			}
			else
			{
			}

			if (NetworkClient.active && !ClientScene.ready)
			{
				if (GUI.Button(new Rect(xpos, ypos, width, height), "Client Ready",guiStyle))
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
				if (GUI.Button(new Rect(xpos+200,height-(height/2), width/2, height), "QUIT",guiStyle))
				{
					manager.StopHost();
				}
				ypos += spacing;
			}

			if (!NetworkServer.active && !NetworkClient.active)
			{
				ypos += (float)(screenHeight/19.2);

				if (manager.matchMaker == null)
				{
					if (GUI.Button(new Rect(xpos, (screenHeight/2)-height, width, height), "Enable Match Maker",guiStyle))
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
							if (GUI.Button(new Rect(xpos,screenHeight/4, width, height), "Create Internet Match",guiStyle))
							{
								manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", manager.OnMatchCreate);
							}
							ypos = (screenHeight / 4);
							ypos += spacing;

							GUI.Label(new Rect(xpos, ypos, width, height), "Room Name:",guiStyle);
							manager.matchName = GUI.TextField(new Rect(xpos, (ypos+(float)(screenHeight/19.2)), width, height), manager.matchName,textField);
							ypos += spacing;

							ypos += (float)(screenHeight/19.2);

							if (GUI.Button(new Rect(xpos, ypos, width, height), "Find Internet Match",guiStyle))
							{
								manager.matchMaker.ListMatches(0,20, "", manager.OnMatchList);
							}
							ypos += spacing;
						}
						else
						{
							foreach (var match in manager.matches)
							{
								if (GUI.Button(new Rect(xpos, ypos, width, height), "Join Match:" + match.name,guiStyle))
								{
									manager.matchName = match.name;
									manager.matchSize = (uint)match.currentSize;
									manager.matchMaker.JoinMatch(match.networkId, "", manager.OnMatchJoined);
								}
								ypos += spacing;
							}
						}
					}

					if (GUI.Button(new Rect(xpos, ypos, width, height), "Disable Match Maker",guiStyle))
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