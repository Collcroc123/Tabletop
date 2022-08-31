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
        playerCountTxt = GameObject.Find("/Canvas/Menu/PLAYERS/Player Count").GetComponent<TextMeshProUGUI>();
        playerCountTxt.text = playerCount + "/" + maxConnections;
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
    {
        base.OnServerSceneChanged("OnlineScene");
        PlayerCounter(0);
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        PlayerCounter(1);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        PlayerCounter(-1);
    }
    
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        PlayerCounter(1);
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        PlayerCounter(-1);
    }
    #endregion
}
