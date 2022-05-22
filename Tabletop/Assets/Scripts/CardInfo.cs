using UnityEngine;

public class CardInfo : MonoBehaviour
{
    public UnoCard cardData;
    public SpriteRenderer cardImage;

    void Start()
    {
        cardImage.sprite = cardData.image;
    }
}