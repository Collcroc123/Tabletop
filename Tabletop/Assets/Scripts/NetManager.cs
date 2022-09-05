using UnityEngine;
using Mirror;
using TMPro;

public class NetManager : NetworkManager
{
    public GameObject news, settings;
    private bool toggled;
    private TextMeshProUGUI playerCountTxt;
    private int playerCount;

    private void PlayerCounter(int num)
    {
        Debug.Log("CONNECTION EVENT");
        playerCount += num;
        playerCountTxt = GameObject.Find("/Table/Menu/Blue Window/Player Count").GetComponent<TextMeshProUGUI>();
        playerCountTxt.text = "Players: " + playerCount + "/" + maxConnections;
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
        PlayerCounter(0);
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    { // When a client joins the SERVER
        base.OnServerConnect(conn);
        PlayerCounter(1);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    { // When a client leaves the SERVER
        base.OnServerDisconnect(conn);
        PlayerCounter(-1);
    }
    
    public override void OnClientConnect()
    { // When the CLIENT joins a server
        base.OnClientConnect();
        //PlayerCounter(1);
    }

    public override void OnClientDisconnect()
    {// When the CLIENT leaves a server
        base.OnClientDisconnect();
        //PlayerCounter(-1);
    }
    #endregion
}
