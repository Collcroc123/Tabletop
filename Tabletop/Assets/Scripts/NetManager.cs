using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.SceneManagement;

public class NetManager : NetworkManager
{
    //[Header("UI")]
    public GameObject news, settings;
    private TextMeshProUGUI playerCountTxt;
    private bool toggled;
    private GameManager gManager;

    //[Scene] [SerializeField] private string menuScene = string.Empty;
    //[SerializeField] private PlayerManager playerPrefab = null;

    public void ToggleSettings()
    {
        toggled = !toggled;
        settings.SetActive(toggled);
        news.SetActive(!toggled);
    }
    
    public void ConnectionEvent()
    {
        Debug.Log("CONNECTION EVENT");
        playerCountTxt.text = "Players: " + numPlayers + "/" + maxConnections;
        GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < array.Length; i++) 
            gManager.playerList[i] = array[i].GetComponent<PlayerManager>();
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    #region Overrides
    public override void OnServerSceneChanged(string sceneName)
    { // When the SERVER starts
        base.OnServerSceneChanged("OnlineScene");
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
        ConnectionEvent();
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    { // When a client leaves the SERVER
        base.OnServerDisconnect(conn);
        ConnectionEvent();
    }
    #endregion
}