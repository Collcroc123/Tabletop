using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour
{
    public Transform hand;
    private GameObject seat;
    private Color bgColor;
    
    void Start()
    {
        if (isLocalPlayer)
        {
            Camera.main.gameObject.transform.SetParent(transform);
            Camera.main.gameObject.transform.localPosition = new Vector3(0,0,-100f);
        }
    }
    
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

    public void PickIcon(Image background)
    {
        bgColor = background.color;
    }

    [ClientRpc]
    private void RpcSyncInfo()
    {
        
    }
}
