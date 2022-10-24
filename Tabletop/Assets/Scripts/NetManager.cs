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
    //public bool gameStart;
    
    [Scene] [SerializeField] private string menuScene = string.Empty;
    [SerializeField] private PlayerManager playerPrefab = null;
    
    private void PlayerCounter()
    {
        Debug.Log("CONNECTION EVENT");
        playerCountTxt = GameObject.Find("/Table/Menu/Blue Window/Player Count").GetComponent<TextMeshProUGUI>();
        playerCountTxt.text = "Players: " + numPlayers + "/" + maxConnections;
    }

    public void ToggleSettings()
    {
        toggled = !toggled;
        settings.SetActive(toggled);
        news.SetActive(!toggled);
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    #region Overrides
    public override void OnServerSceneChanged(string sceneName)
    { // When the SERVER starts
        base.OnServerSceneChanged("OnlineScene");
        PlayerCounter();
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
        PlayerCounter();
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    { // When a client leaves the SERVER
        base.OnServerDisconnect(conn);
        PlayerCounter();
    }
    #endregion
}
