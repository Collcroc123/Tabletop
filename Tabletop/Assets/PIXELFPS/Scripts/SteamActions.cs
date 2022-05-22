using UnityEngine;
//using Steamworks;
using Mirror;
//using Mirror.FizzySteam;
using UnityEngine.UI;

public class SteamActions : MonoBehaviour
{ // https://www.youtube.com/watch?v=QlbBC07dqnE
    /*
    private NetworkActions netManager;
    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;
    private const string HostAddressKey = "HostAddress";
    public Button steamHost;

    public static CSteamID LobbyId { get; private set; }
    
    void Start()
    {
        netManager = GetComponent<NetworkActions>();
        if (SteamManager.Initialized)
        {
            Debug.Log("STEAM IS RUNNING");
            lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
            gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
            lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
            steamHost.interactable = true;
        }
    }
    
    public void HostLobby()
    {
        Debug.Log("HOSTING STEAM LOBBY");
        if (gameObject.GetComponent<FizzySteamworks>() == null) gameObject.AddComponent<FizzySteamworks>();
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, netManager.maxConnections);
    }
    
    public void JoinLobby()
    { // HOW?
        Debug.Log("JOINING STEAM LOBBY");
        //SteamMatchmaking.JoinLobby();
    }
    
    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK) netManager.Error("COULD NOT CREATE LOBBY");
        LobbyId = new CSteamID(callback.m_ulSteamIDLobby);
        netManager.StartHost();
        SteamMatchmaking.SetLobbyData(LobbyId, HostAddressKey, SteamUser.GetSteamID().ToString());
        //OnSteamLobbyCreated.RaiseAction();
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (!NetworkServer.active)
        {
            string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
            netManager.networkAddress = hostAddress;
            netManager.StartClient();
        }
    }

    public void OnPlayerAdded(NetworkConnection conn)
    {
        if (SteamManager.Initialized)
        {
            CSteamID steamId = SteamMatchmaking.GetLobbyMemberByIndex(SteamActions.LobbyId, netManager.numPlayers - 1);
            var player = conn.identity.GetComponent<PlayerMovement>().player;
            player.SetSteamId(steamId.m_SteamID);
        }
    }*/
}