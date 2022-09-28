using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class NetManager : NetworkManager
{
    public GameObject news, settings;//, entryPrefab;
    private TextMeshProUGUI playerCountTxt;
    //private GameObject playerEntry;
    private int playerCount;
    private bool toggled;
    
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
        //playerEntry = Instantiate(entryPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        //playerEntry.transform.parent = GameObject.Find("/Table/Menu/Blue Window/Players List/Viewport/Content/").transform;
        //playerEntry.transform.localScale = new Vector3(1,1,1);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    { // When a client leaves the SERVER
        base.OnServerDisconnect(conn);
        PlayerCounter(-1);
        //Destroy(playerEntry);
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
