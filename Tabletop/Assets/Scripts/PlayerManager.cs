using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    /*public void StartGame()
    {
        handScreen.SetActive(true);
        waitScreen.SetActive(false);
    }*/

    public override void OnStartClient()
    { // Called on the CLIENT when it joins a server
        Debug.Log("JOINING SERVER");
        base.OnStartClient();
        if (isLocalPlayer)
        {
            Camera.main.gameObject.transform.SetParent(transform);
            Camera.main.gameObject.transform.localPosition = new Vector3(0,0,-100f);
        }
    }
}