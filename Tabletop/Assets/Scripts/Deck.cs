using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    public GameObject cardPrefab;
    public List<GameObject> deck = new List<GameObject>();
    public GameObject hand;

    private void Start()
    {
        CreateDeck();
    }

    public void CreateDeck()
    {
        int deckSize = 108; // How many cards in deck
        // Current Card | # of Current Card | Card Class
        int cardNumber = 0, cardNumCount = 0, cardColor = 0;
        Debug.Log("Beginning Deck Creation...");
        for (int i = 0; i < deckSize; i++)
        {
            GameObject card = Instantiate(cardPrefab, hand.transform);
            Card cardInfo = card.GetComponent<Card>();
            cardInfo.color = cardColor;   // Red = 0, Blue = 1, Green = 2, Yellow = 3, Black = 4
            cardInfo.number = cardNumber; // 0-9, 10 = +2, 11 = Reverse, 12 = Skip, Blacks (0 = Wild, 1 = +4)
            //cardInfo.GetImage();
            //card.SetActive(false);
            deck.Add(card);
            Debug.Log("Color: " + cardColor + " Number: " + cardNumber);
            if (cardColor != 4)
            { // If cards are NOT BLACK
                if (cardNumber == 0) cardNumber++; // Makes sure there's only one ZERO card per color
                else cardNumCount++; // Makes two of all NON-ZERO cards
                if (cardNumCount == 2)
                { // Makes sure there's ONLY TWO of each NON-ZERO card per color
                    cardNumCount = 0;
                    cardNumber++;
                    if (cardNumber > 12)
                    { // Changes card color when finished with current color
                        cardNumber = 0;
                        cardColor++;
                    }
                }
            }
            else
            { // If cards are BLACK
                cardNumCount++;
                if (cardNumCount == 4)
                { // Makes sure there's ONLY FOUR of each BLACK card
                    cardNumCount = 0;
                    if (cardNumber == 1)
                    { // Changes card color when finished with current color
                        cardNumber = 0;
                        cardColor++;
                    }
                    else cardNumber++;
                }
            }
        }
    }

    public void Shuffle(Deck deckObj)
    {
        for (int i = 0; i < deckObj.deck.Count; i++) 
        {
            GameObject temp = deckObj.deck[i];
            int randomIndex = Random.Range(i, deckObj.deck.Count);
            deckObj.deck[i] = deckObj.deck[randomIndex];
            deckObj.deck[randomIndex] = temp;
        }
    }
}