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
    /*
    public void StartGame()
    {
        handScreen.SetActive(true);
        waitScreen.SetActive(false);
    }*/

    public override void OnStartServer()
    { // Called on the SERVER when a client joins
        Debug.Log("A CLIENT HAS JOINED THE SERVER");
        base.OnStartServer();
        NetManager.instance.playerList.Add(this);
    }

    public override void OnStartClient()
    { // Called on the CLIENT when it joins a server
        Debug.Log("JOINING SERVER");
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
    { // Called on the SERVER when a client leaves
        Debug.Log("A CLIENT HAS LEFT THE SERVER");
        base.OnStopServer();
        NetManager.instance.playerList.Remove(this);
        Destroy(entry.transform.gameObject);
    }

    public override void OnStopClient()
    { // Called on the CLIENT when it leaves a server
        Debug.Log("LEAVING SERVER");
        base.OnStopClient();
    }
}