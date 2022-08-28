using UnityEngine;

namespace Mirror.Examples.NetworkRoom
{
    [AddComponentMenu("")]
    public class NetworkRoomManagerExt : NetworkRoomManager
    {
        // Called just after GamePlayer object is instantiated and just before it replaces RoomPlayer object.
        // This is the ideal point to pass any data like player name, credentials, tokens, colors, etc.
        // into the GamePlayer object as it is about to enter the Online scene.
        /// <param name="roomPlayer"></param>
        /// <param name="gamePlayer"></param>
        /// <returns>true unless some code in here decides it needs to abort the replacement</returns>
        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            PlayerScore playerScore = gamePlayer.GetComponent<PlayerScore>();
            playerScore.index = roomPlayer.GetComponent<NetworkRoomPlayer>().index;
            return true;
        }


        // Demo for how to do a Start button that only appears for the Host player
        // showStartButton is a local bool needed because OnRoomServerPlayersReady only fires when all players ready
        // If player cancels ready, there's no callback to set false
        // allPlayersReady is used with showStartButton to show/hide Start button
        // Setting showStartButton false when button is pressed hides it in game
        // since NetworkRoomManager is set as DontDestroyOnLoad = true.
        

        bool showStartButton;

        public override void OnRoomServerPlayersReady()
        { // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
        #if UNITY_SERVER
            base.OnRoomServerPlayersReady();
        #else
            showStartButton = true;
        #endif
        }

        public override void OnGUI()
        {
            base.OnGUI();

            if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
            {
                // set to false to hide it in the game scene
                showStartButton = false;

                ServerChangeScene(GameplayScene);
            }
        }
    }
}
