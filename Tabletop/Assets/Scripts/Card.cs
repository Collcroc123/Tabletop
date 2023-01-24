using UnityEngine;
using UnityEditor;

public class Card : MonoBehaviour
{
    [Range(0, 4)]
    public int color; // Red = 0, Blue = 1, Green = 2, Yellow = 3, Black = 4
    [Range(0, 12)]
    public int number; // 0-9, 10 = +2, 11 = Reverse, 12 = Skip, Blacks (0 = Wild, 1 = +4)
    public bool cardBack;

    public CardData data;
    public SpriteRenderer cardImage;
    public Material shader;

    private void Start()
    {
        SetMat();
    }

    private void OnValidate()
    {
        SetMat();
    }

    /*public void GetImage()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Uno-Sheet");
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == "u" + color + "-" + number) cardImage.sprite = sprites[i];
        }
    }*/

    public void SetMat()
    {
        if (cardBack)
        {
            cardImage.sprite = data.backSprite;
            return;
        }

        cardImage.sprite = data.colorSprite;

        if (color != 4)
        {
            shader.SetColor("_Color", data.colors[color]);
            shader.SetTexture("_Number", data.numberSprite[number].texture);
        }
        else 
        {
            shader.SetColor("_Color1", data.colors[0]);
            shader.SetColor("_Color2", data.colors[1]);
            shader.SetColor("_Color3", data.colors[2]);
            shader.SetColor("_Color4", data.colors[3]);
            
            if (number <= 1) shader.SetTexture("_Number", data.numberSprite[number+13].texture);
            else shader.SetTexture("_Number", data.numberSprite[14].texture);
        }
    }
}