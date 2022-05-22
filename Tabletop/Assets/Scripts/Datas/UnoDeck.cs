using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Decks/Uno")]
public class UnoDeck : ScriptableObject
{
    public UnoCard[] deck;
    public bool blanks;
    
    public void CreateUnoDeck(int deckSize)
    {
        int cardNumber=0, cardNumCount=0, cardColor=0;
        if (blanks) { deckSize += 4; }
        Debug.Log("Beginning Deck Creation...");
        deck = new UnoCard[deckSize];
        for (int i = 0; i < deckSize; i++)
        {
            deck[i] = ScriptableObject.CreateInstance<UnoCard>();
            deck[i].color = cardColor;
            deck[i].number = cardNumber;
            deck[i].GetImage();
            Debug.Log("Color: " + cardColor + " Number: " + cardNumber);
            if (cardColor > 0)
            { // If cards are not black
                if (cardNumber == 0)
                { // Makes sure there's only one 0 card per color
                    cardNumber++;
                }
                else
                { // Makes two of all other cards
                    cardNumCount++;
                }
            
                if (cardNumCount == 2)
                { // Makes sure there's only two of each card per color
                    cardNumCount = 0;
                    cardNumber++;
                }
            
                if (cardNumber > 12)
                { // Changes card color when finished with current color
                    cardNumber = 0;
                    cardColor++;
                }
            }
            else
            { // If cards are black
                cardNumCount++;
                if (cardNumCount == 4)
                { // Makes sure there's only four of each black card
                    cardNumCount = 0;
                    cardNumber++;
                }
                if (cardNumber == 2 && !blanks)
                { // Ends black cards if no blanks
                    cardColor++;
                    cardNumber = 0;
                }
                else if (cardNumber == 3 && blanks)
                { // Ends black cards if blanks
                    cardColor++;
                    cardNumber = 0;
                }
            }
        }
    }
}