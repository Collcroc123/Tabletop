using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Mirror;
using TMPro;

public class NetManager : NetworkManager
{
    [Header("UI")]
    public GameObject newsMenu;
    public GameObject settingsMenu;
    private bool toggled;
    private TextMeshProUGUI playerCountTxt;
    public List<PlayerManager> playerList = new List<PlayerManager>();

    [Header("Client Settings")]
    public string roomCode;
    public string userName;
    public Color iconColor;
    public TextMeshProUGUI errorMessage;

    public static NetManager instance;
    public override void Awake()
    {
        base.Awake();
        instance = this;
        //netMan = GameObject.Find("/NetworkManager").GetComponent<NetManager>();
        //gMan = GameObject.Find("/Table").GetComponent<GameManager>();
    }
    
    public void SetRoomCode(TMP_InputField input)
    {
        roomCode = input.text;
    }

    public void SetUsername(TMP_InputField input)
    {
        userName = input.text;
    }
    
    public void SetIcon(Image bg)
    {
        iconColor = bg.color;
    }

    public void JoinGame()
    {
        if (userName == "" || iconColor == new Color(0f, 0f, 0f, 0f))
        {
            errorMessage.transform.gameObject.SetActive(true);
            errorMessage.text = "Select an Icon and Username!";
        }
        else
        {
            //SCAN FOR SERVERS WITH ROOM CODE
            /*if (roomCode == "")
            {
                errorMessage.transform.gameObject.SetActive(true);
                errorMessage.text = "Invalid Room Code!";
            }*/
            //else
            StartClient();
        }
    }

    public void ToggleSettings()
    {
        toggled = !toggled;
        settingsMenu.SetActive(toggled);
        newsMenu.SetActive(!toggled);
    }
    
    public void ConnectionEvent()
    {
        Debug.Log("CONNECTION EVENT");
        playerCountTxt.text = "Players: " + numPlayers + "/" + maxConnections;
    }

    public void StartGame()
    {
        if (numPlayers == 0) Debug.Log("NO PLAYERS PRESENT!");
        else
        {
            foreach (var player in playerList) player.StartGame();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    #region Overrides
    public override void OnServerSceneChanged(string sceneName)
    { // Called on the SERVER when it starts
        base.OnServerSceneChanged("OnlineScene");
        Debug.Log("SERVER STARTED!");
        Debug.Log(numPlayers + " PLAYERS");
        playerCountTxt = GameObject.Find("/Table/Menu/Blue Window/Player Count").GetComponent<TextMeshProUGUI>();
        playerCountTxt.text = "Players: " + numPlayers + "/" + maxConnections;
    }
    
    public override void OnClientConnect()
    { // Called on CLIENTS when they join a server
        //base.OnClientConnect(NetworkClient.connection);
        if (numPlayers >= maxConnections)
        {
            NetworkClient.connection.Disconnect();
            Debug.Log("DISCONNECTED: SERVER FULL");
            return;
        }

        if (SceneManager.GetActiveScene().name != "OnlineScene")
        {
            NetworkClient.connection.Disconnect();
            Debug.Log("DISCONNECTED: GAME IN PROGRESS");
            return;
        }
    }
    
    public override void OnClientDisconnect()
    { // Called on CLIENTS when they leaves a server
        base.OnClientDisconnect();
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    { // Called on the SERVER when a client joins
        base.OnServerConnect(conn);
        //ConnectionEvent(true);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    { // Called on the SERVER when a client leaves
        base.OnServerDisconnect(conn);
        ConnectionEvent();
    }
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    { // Called on the SERVER when a player object loads
        base.OnServerAddPlayer(conn);
        ConnectionEvent();
    }
    #endregion
}