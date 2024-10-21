using System;
using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilla;
using static UnityEngine.UIElements.UxmlAttributeDescription;


namespace GUIModdingTemplate
{

	/// <summary>
	/// This is your mod's main class.
	/// </summary>

	/* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class GUITemplate : BaseUnityPlugin
	{
		bool inRoom;
		bool GUIEnabled = false;





		void Start()
		{
			/* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnEnable()
		{
			/* Set up your mod here */
			/* Code here runs at the start and whenever your mod is enabled*/

			HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
			/* Undo mod setup here */
			/* This provides support for toggling mods with ComputerInterface, please implement it :) */
			/* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

			HarmonyPatches.RemoveHarmonyPatches();
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
			/* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
		}

		void Update()
		{
			if (inRoom || !PhotonNetwork.InRoom)
			{
				if (Keyboard.current.tabKey.wasPressedThisFrame)
				{
					GUIEnabled = !GUIEnabled;
				}

			}
		}


			/* This attribute tells Utilla to call this method when a modded room is joined */
			[ModdedGamemodeJoin]
			public void OnJoin(string gamemode)
			{
				/* Activate your mod here */
				/* This code will run regardless of if the mod is enabled*/

				inRoom = true;
			}

			/* This attribute tells Utilla to call this method when a modded room is left */
			[ModdedGamemodeLeave]
			public void OnLeave(string gamemode)
			{


				inRoom = false;
			}
		private string room = "";

		private void OnGUI()
		{
			if (GUIEnabled)
			{
				GUI.Box(new Rect(10, 10, 150, 400), "SET TO UI NAME");

				room = GUI.TextField(new Rect(15, 50, 140, 30), room, 25);

				if (GUI.Button(new Rect(15, 100, 140, 40), "Join Room")) //join room mod, dont touch
				{

					if (!string.IsNullOrEmpty(room))
					{
						PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(room, JoinType.Solo);
					}
					else
					{
						Debug.Log("Room name cannot be empty.");
					}

				}

				if (GUI.Button(new Rect(15, 150, 140, 40), "Set to Casual"))
				{
                    //mod code
                    //example mod "set to casual" sets the seleted game mode to casual
					GorillaComputer.instance.currentGameMode.Value = "CASUAL";
					/* other game modes:
					- MODDED_CASUAL
					- INFECTION
					- MODDED_INFECTION
					- ect u get the idea*/
                }

                if (GUI.Button(new Rect(15, 250, 140, 40), "Placeholder"))//set placeholder to mod name
                {
                    //mod code
                }

                if (GUI.Button(new Rect(15, 300, 140, 40), "Placeholder"))//set placeholder to mod name
                {
                    //mod code
                }
                if (GUI.Button(new Rect(15, 350, 140, 40), "Placeholder"))//set placeholder to mod name
                {
                    //mod code
                }




            }
		}
	} 
}
