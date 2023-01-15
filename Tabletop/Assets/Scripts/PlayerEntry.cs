using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEntry : MonoBehaviour
{
    public void SetInfo(string name, Color icon)
    {
        transform.GetChild(0).GetComponent<Image>().color = icon;
        transform.GetChild(1).GetComponent<TMP_Text>().text = name; 
    }
}