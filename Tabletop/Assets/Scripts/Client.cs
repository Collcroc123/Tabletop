using UnityEngine;
using Mirror;

public class Client : NetworkBehaviour
{
    public ServerManager manager;
    public Connector connector;

    public void DrawCards(int num)
    {
        connector.Draw(num);
    }
    
    public void EndTurn()
    {
        // Fade, disable ability to play cards
    }

    public void PlayCard(GameObject card)
    {
        UnoCard cardData = card.GetComponent<CardInfo>().cardData;
        connector.Play(cardData);
        Destroy(card);
        
        // Client tells server the card
        // Server receives card, responds if received
        // Client gets rid of card if server received
        // Server plays the card
    }
}
