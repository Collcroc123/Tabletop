using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public GameObject playerList;
    public GameObject cardDeck;

    public static GameManager instance;
    void Awake()
    {
        instance = this;
    }
    /*
    public void StartGame()
    {
        NetManager.instance.StartGame();
        cardDeck.SetActive(true);
    }*/
}