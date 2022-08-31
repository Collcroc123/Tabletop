using UnityEngine;

[CreateAssetMenu(menuName = "Datas/BoolData")]
public class BoolData : ScriptableObject
{
    public bool var;

    public void Toggle()
    {
        var = !var;
    }
}