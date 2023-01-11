using UnityEngine;

public class GameManager : MonoBehaviour
{
    private NetManager netMan;
    public GameObject cardDeck;

    void Start()
    {
        GameObject network = GameObject.Find("/NetworkManager/");
        netMan = network.GetComponent<NetManager>();
    }

    public void StartGame()
    {
        cardDeck.SetActive(true);
    }
}
