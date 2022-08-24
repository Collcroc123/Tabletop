using UnityEngine;

public class Card : MonoBehaviour
{
    public int color; // Black=0, Red=1, Blue=2, Green=3, Yellow=4
    public int number; // 0-9, +2=10, Reverse=11, Skip=12, Blacks(Wild=0, +4=1, Blank=2)
    public SpriteRenderer cardImage;

    public void GetImage()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Uno-Sheet");
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == "u" + color + "-" + number) cardImage.sprite = sprites[i];
        }
    }
}