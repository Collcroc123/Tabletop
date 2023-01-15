using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public GameObject waitScreen, handScreen;

    [SyncVar] public string userName;
    [SyncVar] public Color iconColor;
    public GameObject entryPrefab;
    private PlayerEntry entry = null;

    [Command]
    public void CmdPlayerEntry(string name, Color icon)
    { // Creates a player entry in the players list on the server
        userName = name;
        iconColor = icon;
        entry = Instantiate(entryPrefab, GameManager.instance.playerList.transform).GetComponent<PlayerEntry>();
        entry.SetInfo(userName, iconColor);
    }

    public void StartGame()
    {
        handScreen.SetActive(true);
        waitScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public override void OnStartServer()
    {
        Debug.Log("OnStartServer");
        base.OnStartServer();
        NetManager.instance.playerList.Add(this);
    }

    public override void OnStartClient()
    {
        Debug.Log("OnStartClient");
        base.OnStartClient();
        if (isLocalPlayer)
        {
            Camera.main.gameObject.transform.SetParent(transform);
            Camera.main.gameObject.transform.localPosition = new Vector3(0,0,-100f);
        }
        userName = NetManager.instance.userName;
        iconColor = NetManager.instance.iconColor;
        CmdPlayerEntry(userName, iconColor);
    }

    public override void OnStopServer()
    {
        Debug.Log("OnStopServer");
        base.OnStopServer();
        NetManager.instance.playerList.Remove(this);
        Destroy(entry.transform.gameObject);
    }

    public override void OnStopClient()
    {
        Debug.Log("OnStopClient");
        base.OnStopClient();
    }
}