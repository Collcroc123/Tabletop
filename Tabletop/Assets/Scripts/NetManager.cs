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
    private GameManager gManager;
    private TextMeshProUGUI playerCountTxt;
    public List<PlayerManager> playerList = new List<PlayerManager>();

    [Header("Client Settings")]
    public string roomCode;
    public string username;
    public Color iconColor;
    public TextMeshProUGUI errorMessage;
    //private const string PlayerNameKey = "PlayerName";
    
    public void SetRoomCode(TMP_InputField input)
    {
        roomCode = input.text;
    }

    public void SetUsername(TMP_InputField input)
    {
        username = input.text;
        //PlayerPrefs.SetString(PlayerNameKey, username);
    }
    
    public void SetIcon(Image bg)
    {
        iconColor = bg.color;
    }

    public void JoinGame()
    {
        if (username == "" || iconColor == new Color(0f, 0f, 0f, 0f))
        {
            errorMessage.transform.gameObject.SetActive(true);
            errorMessage.text = "Select an Icon and Username!";
        }
        else
        {
            //SCAN FOR SERVERS WITH ROOM CODE
            if (roomCode == "")
            {
                errorMessage.transform.gameObject.SetActive(true);
                errorMessage.text = "Invalid Room Code!";
            }
            else StartClient();       
        }
    }

    public void ToggleSettings()
    {
        toggled = !toggled;
        settingsMenu.SetActive(toggled);
        newsMenu.SetActive(!toggled);
    }
    
    public void ConnectionEvent(bool joined)
    {
        Debug.Log("CONNECTION EVENT");
        Debug.Log(numPlayers + " PLAYERS");
        playerCountTxt.text = "Players: " + numPlayers + "/" + maxConnections;
        playerList.Clear();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            PlayerManager pMan = player.GetComponent<PlayerManager>();
            playerList.Add(pMan);
            if (joined) pMan.CmdPlayerEntry(username, iconColor);
        }
    }

    public void StartGame()
    {
        if (numPlayers == 0)
            Debug.Log("NO PLAYERS PRESENT!");
        else
        {
            foreach (var player in playerList)
            {
                player.StartGame();
            }
            gManager.StartGame();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    #region Overrides
    public override void OnServerSceneChanged(string sceneName)
    { // When the SERVER starts
        base.OnServerSceneChanged("OnlineScene");
        Debug.Log("SERVER STARTED!");
        Debug.Log(numPlayers + " PLAYERS");
        playerCountTxt = GameObject.Find("/Table/Menu/Blue Window/Player Count").GetComponent<TextMeshProUGUI>();
        playerCountTxt.text = "Players: " + numPlayers + "/" + maxConnections;
        gManager = GameObject.Find("Table").GetComponent<GameManager>();
    }
    
    public override void OnClientConnect(NetworkConnection conn)
    { // When the CLIENT joins a server
        base.OnClientConnect(conn);
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            Debug.Log("DISCONNECTED: SERVER FULL");
            return;
        }

        if (SceneManager.GetActiveScene().name != "OnlineScene")
        {
            conn.Disconnect();
            Debug.Log("DISCONNECTED: GAME IN PROGRESS");
            return;
        }
    }
    
    public override void OnClientDisconnect()
    { // When the CLIENT leaves a server
        base.OnClientDisconnect();
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    { // When a client joins the SERVER
        base.OnServerConnect(conn);
        ConnectionEvent(true);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    { // When a client leaves the SERVER
        base.OnServerDisconnect(conn);
        ConnectionEvent(false);
    }
    #endregion
}