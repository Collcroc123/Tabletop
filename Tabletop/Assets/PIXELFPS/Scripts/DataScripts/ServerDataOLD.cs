using Mirror;
using Mirror.Discovery;
using UnityEngine;

//[CreateAssetMenu(menuName = "Datas/ServerData")]
public class ServerDataOLD : ScriptableObject
{
    //[HideInInspector] public ServerResponse info;
    [Tooltip("Server name")]
    public string serverName;
    [Tooltip("Password needed to join server")]
    public string password;
    [Tooltip("Max number players that can join")]
    public int maxPlayers = 4;

    [Tooltip("What % the player speed is multiplied")] 
    [Range(25,300)] public int playerSpeedMultiplier = 100;
    [Tooltip("What % the gravity is multiplied")] 
    [Range(25, 300)] public int gravityMultiplier = 100;
    [Tooltip("How long after death until the player respawns")] 
    [Range(0,30)] public int respawnTime = 5;
}