using UnityEngine;
using Mirror;

public class PlayerOld : NetworkBehaviour
{
    public PlayerData player;
    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            transform.position = transform.position + movement;
        }
    }

    void Update()
    {
        HandleMovement();

        if (isLocalPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Saying Hello to Server!");
            Hello();
        }
    }

    public override void OnStartServer()
    {
        Debug.Log("Player has spawned on the server!");
    }

    [Command]
    void Hello()
    {
        Debug.Log("Received Hello from Client, Reply with X!");
        HelloBack();
    }
    
    [TargetRpc]
    void HelloBack()
    {
        Debug.Log("Received Hello from Server!");
    }
}
