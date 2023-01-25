using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Datas/Card")]
public class CardData : ScriptableObject
{
    public Color[] colors;
    public Sprite backSprite;
    public Sprite colorSprite;
    public Sprite[] numberSprite;
}