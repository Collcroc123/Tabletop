using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject[] deck;
    public GameObject card;

    private void Start()
    {
        CreateUnoDeck();
    }

    public void CreateUnoDeck()
    {
        int deckSize = 108; // How many cards in deck
        // Current Card | # of Current Card | Card Class
        int cardNumber=0, cardNumCount=0, cardColor=0;
        Debug.Log("Beginning Deck Creation...");
        deck = new GameObject[deckSize];
        for (int i = 0; i < deckSize; i++)
        {
            deck[i] = Instantiate(card);
            deck[i].GetComponent<Card>().color = cardColor;   // Black=0, Red=1, Blue=2, Green=3, Yellow=4
            deck[i].GetComponent<Card>().number = cardNumber; // 0-9, +2=10, Reverse=11, Skip=12, Blacks(Wild=0, +4=1, Blank=2)
            deck[i].GetComponent<Card>().GetImage();
            deck[i].SetActive(false);
            Debug.Log("Color: " + cardColor + " Number: " + cardNumber);
            if (cardColor > 0)
            { // If cards are not black
                if (cardNumber == 0) cardNumber++; // Makes sure there's only one 0 card per color
                else cardNumCount++; // Makes two of all other cards
                if (cardNumCount == 2)
                { // Makes sure there's only two of each card per color
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
            { // If cards are black
                cardNumCount++;
                if (cardNumCount == 4)
                { // Makes sure there's only four of each black card
                    cardNumCount = 0;
                    if (cardNumber == 1)
                    {
                        cardColor++;
                        cardNumber = 0;
                    }
                    else cardNumber++;
                }
            }
        }
    }
}