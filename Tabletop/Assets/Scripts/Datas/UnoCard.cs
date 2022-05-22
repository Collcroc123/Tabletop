using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Uno")]
public class UnoCard : ScriptableObject
{
    public int color; // Black = 0, Red = 1, Blue = 2, Green = 3, Yellow = 4
    public int number; // 0-9, +2 = 10, Reverse = 11, Skip = 12, Blacks(Wild = 0, +4 = 1, Blank = 2)
    public Sprite image;

    public void GetImage()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Uno-Sheet");
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == color + "_" + number)
            {
                image = sprites[i];
            }
        }
    }
}