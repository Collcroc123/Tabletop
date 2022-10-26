using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager[] playerList;

    public void StartGame()
    {
        foreach (var player in playerList)
        {
            if (!player.isReady)
            {
                Debug.Log("CONNECTED USER HAS NOT ENTERED A NAME OR ICON");
                return;
            }
        }
        
    }
}
