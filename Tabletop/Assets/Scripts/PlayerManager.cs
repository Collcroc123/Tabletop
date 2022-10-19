using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour
{
    private GameObject entry;
    public GameObject entryPrefab;
    public Color iconColor;
    public string username;
    
    //public GameObject playerMenu, handMenu;
    //public Transform hand;
    
    void Start()
    {
        if (isLocalPlayer)
        {
            Camera.main.gameObject.transform.SetParent(transform);
            Camera.main.gameObject.transform.localPosition = new Vector3(0,0,-100f);
            CmdPlayerEntry();
        }
    }

    [Command]
    public void CmdPlayerEntry()
    {
        entry = Instantiate(entryPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        entry.GetComponent<PlayerEntry>().parentClient = transform.gameObject;
        entry.transform.parent = GameObject.Find("/Table/Menu/Blue Window/Players List/Viewport/Content/").transform;
        entry.transform.localScale = new Vector3(1,1,1);
        //NetworkServer.Spawn(entry);
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
    
    public void PickIcon(Image bg)
    {
        iconColor = bg.color;
        CmdSetIcon(iconColor);
    }

    [Command]
    public void CmdSetIcon(Color bgColor)
    {
        entry.transform.GetChild(0).GetComponent<Image>().color = bgColor;
    }

    public void GetUsername(TMP_InputField input)
    {
        username = input.text;
        CmdSetUsername(username);
    }

    [Command]
    public void CmdSetUsername(string playername)
    {
        entry.transform.GetChild(1).GetComponent<TMP_Text>().text = playername;
    }
    
    /*[ClientRpc]
    public void RpcHandMenu()
    {
        handMenu.SetActive(true);
        playerMenu.SetActive(false);
    }*/
}