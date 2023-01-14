using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class PlayerManager : NetworkBehaviour
{
    private GameObject entry = null;
    public GameObject entryPrefab;
    public GameObject waitScreen, handScreen;
    public NetManager netMan;
    
    void Start()
    {
        if (isLocalPlayer)
        {
            Camera.main.gameObject.transform.SetParent(transform);
            Camera.main.gameObject.transform.localPosition = new Vector3(0,0,-100f);
        }
        if (netMan != null) CmdPlayerEntry(netMan.username, netMan.iconColor);
        else Debug.Log("MISSING NETMAN!");
    }

    [Command]
    public void CmdPlayerEntry(string displayName, Color icon)
    { // Creates a player entry in the players list on the server
        if (entry == null)
        {
            entry = Instantiate(entryPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            entry.GetComponent<PlayerEntry>().parentClient = transform.gameObject;
            entry.transform.SetParent(GameObject.Find("/Table/Menu/Blue Window/Players List/Viewport/Content/").transform);
            entry.transform.localScale = new Vector3(1,1,1);
        }
        entry.transform.GetChild(1).GetComponent<TMP_Text>().text = displayName;
        entry.transform.GetChild(0).GetComponent<Image>().color = icon;
    }

    public void StartGame()
    {
        handScreen.SetActive(true);
        waitScreen.SetActive(false);
    }
    
    /*
    public void PickSeat(int seatNum)
    {
        seat = GameObject.Find("/Seats/Seat " + seatNum);
        if (!seat.GetComponent<PlayerInfo>().isTaken)
        {
            transform.position = seat.transform.position;
            seat.GetComponent<PlayerInfo>().isTaken = true;
        }
        else
        {
            Debug.Log("SEAT #" + seatNum + " IS TAKEN");
        }
    }
    */
    
    /*[ClientRpc]
    public void RpcHandMenu()
    {
        handMenu.SetActive(true);
        playerMenu.SetActive(false);
    }*/
}