using UnityEngine;
using Mirror;

public class Server : NetworkBehaviour
{
    public ServerManager manager;
    public Connector connector;
    private int cardColor, cardNumber;
    private int drawSize = 108, discardSize = 0;
    public bool infiniteDeck, blanks;
    public UnoDeck draw, discard;
    public GameObject card;
    private bool deckCreated, dealt;

    void Update()
    {
        if (NetworkServer.connections.Count >= 4 && !dealt)
        {
            //Deal(7);
        }
    }
    
    public void StartGame()
    {
        draw.CreateUnoDeck(drawSize);
        InstantiateDeck();
    }

    public void CheckCardPlayability(int playedColor, int playedNumber)
    {
        if (playedColor == cardColor || playedNumber == cardNumber)
        {
            // if (color == 0 && number > 3) { Debug.Log("INVALID CARD: " + color + "-" + number); }
            // if (number < 0 || color < 0 || number > 12 || color > 4) { Debug.Log("INVALID CARD: " + color + "-" + number); }
        }
    }
    
    public void InstantiateDeck()
    {
        for (int i = 0; i < draw.deck.Length; i++)
        {
            if (draw.deck[i] != null)
            {
                GameObject newCard = Instantiate(card, new Vector3(-1f, (i*0.005f)-0.5f, i*-0.01f), Quaternion.Euler(0, 0, 0));
                newCard.GetComponent<CardInfo>().cardData = draw.deck[i];
                deckCreated = true;
            }
        }
    }
    
    public UnoCard GetTopCard()
    {
        UnoCard returnCard = null;
        for (int i = 0; i < draw.deck.Length; i++)
        {
            if (draw.deck[i] == null || i == draw.deck.Length - 1)
            {
                returnCard = draw.deck[i - 1];
                draw.deck[i - 1] = null;
                drawSize = i - 2;
                break;
            }
        }
        return returnCard;
    }

    public UnoCard GetCurrentCard()
    {
        UnoCard currentCard = null;
        for (int i = 0; i < discard.deck.Length; i++)
        {
            // NEED TO RETURN IF EMPTY!!!
            if (discard.deck[i] == null || i == discard.deck.Length - 1)
            {
                currentCard = discard.deck[i - 1];
                discardSize = i - 1;
                break;
            }
        }
        return currentCard;
    }
    
    public void Deal(int num)
    {
        for (int k = 0; k < num; k++)
        {
            for (int i = 0; i <= connector.currentPlayerCount; i++)
            {
                Debug.Log("DEAL");
                connector.NewCard(connector.players[i], GetTopCard());
                drawSize--;
                dealt = true;
            }
        }
    }
    
    public void RefillDraw()
    {
        if (!infiniteDeck && drawSize <= 0)
        {
            if (discardSize > 0)
            {
                for (int i = 0; i < draw.deck.Length; i++)
                {
                    if (discard.deck[i] != null)
                    {
                        draw.deck[i] = discard.deck[i];
                        discard.deck[i] = null;
                    }
                    else
                    {
                        break;
                    }
                }
                drawSize = discardSize;
                discardSize = 0;
            }
            else
            {
                Debug.Log("Draw Pile & Discard Pile Both Empty!");
                //EndTurn();
            }
        }
    }
    
    void PlayerSendCard(UnoCard card)
    {
        Instantiate(card, new Vector3(1f, (discardSize*0.005f)-0.5f, discardSize*-0.01f), Quaternion.Euler(0, 0, 0));
        discardSize++;
    }
}
