using UnityEngine;
using Mirror;

public class Connector : NetworkBehaviour
{
    public ServerManager manager;  // Manages incoming and outgoing connections
    public Server server;          // Server script for server commands
    public Client client;          // Client script for client commands
    private float lastCardLoc;     // TEMP! Spawns next card over from last
    public GameObject cardPrefab;  // Blank card prefab for spawning
    
    public NetworkConnection[] players; // Tracks each player's NetworkConnection  = new SyncList<NetworkConnection>()
    [SyncVar] public int currentPlayerCount;      // Tracks how many players are connected
    [SyncVar] public int maxPlayers = 8;          // Limits number of players connected
    public int clientID;                          // Tracks player's order of joining
    
    [Command]
    public void NextTurn()
    {
        server.GetCurrentCard();
        // send to current player
    }
    
    [Command]
    public void Draw(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Debug.Log("DRAWING CARD");
            NewCard(players[clientID], server.GetTopCard());
            server.RefillDraw();
        }
    }
    
    [TargetRpc]
    public void NewCard(NetworkConnection target, UnoCard unoCard)
    {
        if (cardPrefab != null) // Why would CardPrefab ever be null?
        {
            lastCardLoc += 0.5f;
            GameObject newCard = Instantiate(cardPrefab, new Vector3(lastCardLoc, 0, 0), Quaternion.Euler(0, 0, 0));
            newCard.GetComponent<CardInfo>().cardData = unoCard;
        }
        else
        {
            client.EndTurn();
            NextTurn();
        }
    }

    [Command]
    public void Play(UnoCard card)
    {
        // Remove card from player's hand, instantiate card in server
    }



    public override void OnStartServer()
    {
        Debug.Log("STARTING SERVER");
        // players = new NetworkConnection[maxPlayers];
    }

    public override void OnStopServer()
    {
        Debug.Log("STOPPING SERVER");
        currentPlayerCount = 0;
    }

    public void ClientConnected(NetworkConnection client)
    {
        players[currentPlayerCount] = client;
        clientID = currentPlayerCount;
        currentPlayerCount++;
        Debug.Log("CONNECTED TO SERVER, CURRENT PLAYER COUNT: " + currentPlayerCount);
        Debug.Log("Your ClientID is: " + clientID);
    }

    public void ClientDisconnected(NetworkConnection client)
    {
        for (int i = 0; i < maxPlayers; i++)
        {
            if (players[i] == client)
            {
                players[i] = null;
                clientID = 999;
                currentPlayerCount--;
                Debug.Log("DISCONNECTED FROM SERVER, CURRENT PLAYER COUNT: " + currentPlayerCount);
                return;
            }
        }
    }
}