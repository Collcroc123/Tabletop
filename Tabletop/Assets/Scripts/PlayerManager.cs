using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public Transform hand;

    void Start()
    {
        if (isLocalPlayer)
        {
            Camera.main.gameObject.transform.parent = this.transform;
            Camera.main.gameObject.transform.position = new Vector3(transform.position.x,transform.position.y,-0.4f);
        }
    }

    public void PickSeat(int seatNum)
    {
        GameObject seat = GameObject.Find("/Seats/Seat " + seatNum);
        if (!seat.GetComponent<PlayerSeat>().isTaken)
        {
            transform.position = seat.transform.position;
            seat.GetComponent<PlayerSeat>().isTaken = true;
        }
        else
        {
            Debug.Log("SEAT #" + seatNum + " IS TAKEN");
        }
    }
}
