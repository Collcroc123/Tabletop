using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour
{
    public Transform hand;
    private GameObject entry; 
    public GameObject entryPrefab;

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
        CmdSetIcon(bg.color);
    }

    [Command]
    public void CmdSetIcon(Color bgColor)
    {
        entry.transform.GetChild(0).GetComponent<Image>().color = bgColor;
    }

    public void GetUsername(TMP_InputField input)
    {
        CmdSetUsername(input.text);
    }

    [Command]
    public void CmdSetUsername(string name)
    {
        entry.transform.GetChild(1).GetComponent<TMP_Text>().text = name;
    }

    [ClientRpc]
    private void RpcSyncInfo()
    {
        
    }
}
