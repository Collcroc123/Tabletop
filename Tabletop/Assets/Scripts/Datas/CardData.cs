using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Datas/Card")]
public class CardData : ScriptableObject
{
    public Color[] colors;
    public Sprite[] backgroundSprite;
    public Sprite[] borderSprite;
    public Sprite[] numberSprite;
}