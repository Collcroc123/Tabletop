using UnityEngine;

[CreateAssetMenu(menuName = "Datas/StringData")]
public class StringData : ScriptableObject
{
    public string var = "Player";

    public void SetName(string newVar)
    {
        var = newVar;
        if (var == null) var = "NULL";
    }
}