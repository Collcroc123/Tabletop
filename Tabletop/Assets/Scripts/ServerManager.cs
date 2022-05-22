using UnityEngine;
using Mirror;

public class ServerManager : NetworkManager
{
    public Connector connector;
    
    public void OnClientConnect(NetworkConnection client) // override 
    {
        connector.ClientConnected(client);
    }

    public void OnClientDisconnect(NetworkConnection client) // override 
    {
        connector.ClientDisconnected(client);
    }
}