using UnityEngine;
using Mirror;

/*  Documentation: https://mirror-networking.gitbook.io/docs/components/network-room-player
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkRoomPlayer.html

    This component works in conjunction with the NetworkRoomManager to make up the multiplayer room system.
    The RoomPrefab object of the NetworkRoomManager must have this component on it.
    This component holds basic room player data required for the room to function.
    Game specific data for room players can be put in other components on the RoomPrefab or in scripts derived from NetworkRoomPlayer.
*/
public class RoomPlayerManager : NetworkRoomPlayer
{
    [SyncVar] public string userName;
    [SyncVar] public Color iconColor;
    public GameObject entryPrefab;
    private PlayerEntry entry = null;
    
    public override void OnStartServer() 
    { // Called on the SERVER when a client joins
        Debug.Log("A CLIENT HAS JOINED THE SERVER");
        base.OnStartServer();
        NetManager.instance.playerList.Add(this);
        CmdCreateEntry();
    }

    public override void OnStopServer() 
    { // Called on the SERVER when a client leaves
        Debug.Log("A CLIENT HAS LEFT THE SERVER");
        base.OnStopServer();
        NetManager.instance.playerList.Remove(this);
        Destroy(entry.transform.gameObject);
    }

    //[Command]
    public void CmdCreateEntry() 
    {
        userName = NetManager.instance.userName;
        iconColor = NetManager.instance.iconColor;
        entry = Instantiate(entryPrefab, GameManager.instance.playerList.transform).GetComponent<PlayerEntry>();
        entry.SetInfo(userName, iconColor);
    }

    #region Start & Stop Callbacks

    // Called on every NetworkBehaviour when it is activated on a client.
    // Objects on the host have this function called, as there is a local client on the host. 
    // The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.
    public override void OnStartClient() { }

    // This is invoked on clients when the server has caused this object to be destroyed.
    // This can be used as a hook to invoke effects or do client specific cleanup.
    public override void OnStopClient() { }

    // Called when the local player object has been set up.
    // This happens after OnStartClient(), as it is triggered by an ownership message from the server. 
    // This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.
    public override void OnStartLocalPlayer() { }

    // This is invoked on behaviours that have authority, based on context and NetworkIdentity.hasAuthority.
    // This is called after OnStartServer and before OnStartClient.
    // When NetworkIdentity.AssignClientAuthority is called on the server, this will be called on the client that owns the object. 
    // When an object is spawned with NetworkServer.Spawn with a NetworkConnectionToClient parameter included, this will be called on the client that owns the object.
    public override void OnStartAuthority() { }

    // This is invoked on behaviours when authority is removed.
    // When NetworkIdentity.RemoveClientAuthority is called on the server, this will be called on the client that owns the object.
    public override void OnStopAuthority() { }

    #endregion

    #region Room Client Callbacks

    // This is a hook that is invoked on all player objects when entering the room.
    // Note: isLocalPlayer is not guaranteed to be set until OnStartLocalPlayer is called.
    public override void OnClientEnterRoom() { }

    // This is a hook that is invoked on all player objects when exiting the room.
    public override void OnClientExitRoom() { }

    #endregion

    #region SyncVar Hooks

    // This is a hook that is invoked on clients when the index changes.
    public override void IndexChanged(int oldIndex, int newIndex) { }

    // This is a hook that is invoked on clients when a RoomPlayer switches between ready or not ready.
    // This function is called when the a client player calls SendReadyToBeginMessage() or SendNotReadyToBeginMessage().
    public override void ReadyStateChanged(bool oldReadyState, bool newReadyState) { }

    #endregion
}
