using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour
{
    [Header("Player Entry")]
    public GameObject entryPrefab;
    public GameObject iconOutline;
    private GameObject entry = null;
    public string username;
    public Color iconColor;
    public bool isReady;
    private const string PlayerNameKey = "PlayerName";
    
    //public GameObject playerMenu, handMenu;
    //public Transform hand;
    
    void Start()
    {
        if (isLocalPlayer)
        {
            Camera.main.gameObject.transform.SetParent(transform);
            Camera.main.gameObject.transform.localPosition = new Vector3(0,0,-100f);
        }
    }

    public void SetPlayerInfo()
    {
        CmdPlayerEntry(username, iconColor);
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
            entry.transform.GetChild(1).GetComponent<TMP_Text>().text = displayName;
            entry.transform.GetChild(0).GetComponent<Image>().color = icon;
            isReady = true;
        }
        else
        {
            entry.transform.GetChild(1).GetComponent<TMP_Text>().text = displayName;
            entry.transform.GetChild(0).GetComponent<Image>().color = icon;
        }
    }

    public void SetUsername(TMP_InputField input)
    {
        username = input.text;
        PlayerPrefs.SetString(PlayerNameKey, username);
    }
    
    public void SetIcon(Image bg)
    {
        iconColor = bg.color;
        iconOutline.transform.SetParent(bg.transform, false);
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