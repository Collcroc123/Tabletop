using UnityEngine;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-room-player
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkRoomPlayer.html
*/

/// This component works in conjunction with the NetworkRoomManager to make up the multiplayer room system.
/// The RoomPrefab object of the NetworkRoomManager must have this component on it.
/// This component holds basic room player data required for the room to function.
/// Game specific data for room players can be put in other components on the RoomPrefab or in scripts derived from NetworkRoomPlayer.
public class NewNetworkRoomPlayer : NetworkRoomPlayer
{
    #region Start & Stop Callbacks

    /// This is invoked for NetworkBehaviour objects when they become active on SERVER.
    /// <para>This could be triggered by NetworkServer.Listen() for objects in the scene, or by NetworkServer.Spawn() for objects that are dynamically created.</para>
    /// <para>This will be called for objects on a "host" as well as for object on a dedicated server.</para>
    public override void OnStartServer() { }

    /// Invoked on SERVER when the object is unspawned
    /// <para>Useful for saving object data in persistent storage</para>
    public override void OnStopServer() { }

    /// Called on every NetworkBehaviour when it is activated on a client.
    /// <para>Objects on HOST have this function called, as there is a local client on HOST. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on CLIENT.</para>
    public override void OnStartClient() { }

    /// This is invoked on clients when the server has caused this object to be destroyed.
    /// <para>This can be used as a hook to invoke effects or do client specific cleanup.</para>
    public override void OnStopClient() { }

    /// Called when the local player object has been set up.
    /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
    public override void OnStartLocalPlayer() { }

    /// This is invoked on behaviours that have authority, based on context and <see cref="NetworkIdentity.hasAuthority">NetworkIdentity.hasAuthority</see>.
    /// <para>Called after <see cref="OnStartServer">OnStartServer</see> and before <see cref="OnStartClient">OnStartClient.</see></para>
    /// <para>When <see cref="NetworkIdentity.AssignClientAuthority"/> is called on SERVER, this will be called on CLIENT that owns the object. When an object is spawned with <see cref="NetworkServer.Spawn">NetworkServer.Spawn</see> with a NetworkConnectionToClient parameter included, this will be called on CLIENT that owns the object.</para>
    public override void OnStartAuthority() { }

    /// This is invoked on behaviours when authority is removed.
    /// <para>When NetworkIdentity.RemoveClientAuthority is called on SERVER, this will be called on CLIENT that owns the object.</para>
    public override void OnStopAuthority() { }

    #endregion

    #region Room Client Callbacks

    /// Hook that is invoked on all player objects when entering the room.
    /// <para>Note: isLocalPlayer is not guaranteed to be set until OnStartLocalPlayer is called.</para>
    public override void OnClientEnterRoom() { }

    /// Hook that is invoked on all player objects when exiting the room.
    public override void OnClientExitRoom() { }

    #endregion

    #region SyncVar Hooks

    /// Hook that is invoked on clients when the index changes.
    /// <param name="oldIndex">The old index value</param>
    /// <param name="newIndex">The new index value</param>
    public override void IndexChanged(int oldIndex, int newIndex) { }

    /// Hook that is invoked on clients when a RoomPlayer switches between ready or not ready.
    /// <para>This function is called when the a client player calls SendReadyToBeginMessage() or SendNotReadyToBeginMessage().</para>
    /// <param name="oldReadyState">The old readyState value</param>
    /// <param name="newReadyState">The new readyState value</param>
    public override void ReadyStateChanged(bool oldReadyState, bool newReadyState) { }

    #endregion

    #region Optional UI

    public override void OnGUI()
    {
        base.OnGUI();
    }

    #endregion
}
