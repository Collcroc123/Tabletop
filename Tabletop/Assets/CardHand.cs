using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    // Prefab for a card
    public GameObject cardPrefab;

    // Number of cards in the hand
    public int numCards = 5;

    // Width and height of the area in which the cards should be distributed
    public float areaWidth = 5.0f;
    public float areaHeight = 5.0f;

    // Spacing between cards
    public float cardSpacing = 0.1f;

    void Start()
    {
        // Calculate the dimensions of a single card
        float cardWidth = cardPrefab.GetComponent<Renderer>().bounds.size.x;
        float cardHeight = cardPrefab.GetComponent<Renderer>().bounds.size.y;

        // Calculate the number of columns and rows needed to evenly distribute the cards
        int numColumns = Mathf.CeilToInt(Mathf.Sqrt(numCards));
        int numRows = Mathf.CeilToInt((float) numCards / numColumns);

        // Calculate the total width and height of the grid of cards
        float gridWidth = (cardWidth + cardSpacing) * numColumns - cardSpacing;
        float gridHeight = (cardHeight + cardSpacing) * numRows - cardSpacing;

        // Calculate the starting position for the grid of cards
        float startX = -gridWidth / 2.0f;
        float startY = gridHeight / 2.0f;

        // Instantiate the cards
        for (int i = 0; i < numCards; i++)
        {
            // Calculate the position for the current card
            float x = startX + (i % numColumns) * (cardWidth + cardSpacing);
            float y = startY - (i / numColumns) * (cardHeight + cardSpacing);

            // Create a new instance of the card prefab
            GameObject card = Instantiate(cardPrefab, new Vector3(x, y, 0), Quaternion.identity);

            // Set the parent of the card to be the object that this script is attached to
            card.transform.parent = transform;
        }
    }
}
