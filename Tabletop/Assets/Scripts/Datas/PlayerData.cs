using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class PlayerData : ScriptableObject
{
    public string userName;
    public int playerIcon;
    //public Color playerColor;
    public float musicVolume;
    public float soundVolume;
}
/*using Unity.Netcode;

public struct PlayerData : INetworkSerializable
{
    public ulong ID;
    public string Name;
    public bool Ready;

    public LobbyPlayerState(ulong clientId, string playerName, bool isReady)
    {
        clientId = ID;
        playerName = Name;
        isReady = Ready;
    }

    public void NetworkSerialize(INetworkSerializable serializer)
    {
        serializer.Serialize(ref clientId);
        serializer.Serialize(ref playerName);
        serializer.Serialize(ref isReady);
    }
}*/