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
    public Material cardShader;

    private void Start()
    {
        SetMat();
    }

    private void OnValidate()
    {
        SetMat();
    }

    public void SetMat()
    {
        Material shader = Instantiate(cardShader);
        cardImage.material = shader;
        cardImage.sprite = data.colorSprite;

        if (cardBack) shader.SetTexture("_Number", data.backSprite.texture);

        else if (color != 4)
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
            
            if (number >= 1) shader.SetTexture("_Number", data.numberSprite[14].texture);
            else shader.SetTexture("_Number", data.numberSprite[13].texture);
        }
    }

    /*public void GetImage()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Uno");
        if (cardBack)
        {
            cardImage.sprite = sprites[15];
            return;
        }
        cardImage.sprite = sprites[16];

        if (color != 4) 
        {
            shader.SetColor("_Color", data.colors[color]);
            shader.SetTexture("_Number", sprites[number].texture);
        }
        else
        {
            shader.SetColor("_Color1", data.colors[0]);
            shader.SetColor("_Color2", data.colors[1]);
            shader.SetColor("_Color3", data.colors[2]);
            shader.SetColor("_Color4", data.colors[3]);

            if (number >= 1) shader.SetTexture("_Number", sprites[14].texture);
            else shader.SetTexture("_Number", sprites[13].texture);
        }
    }*/
}