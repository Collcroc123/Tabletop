using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool state = true;
    public GameObject hostMenu;
    public GameObject clientMenu;

    void Start()
    {
        if (Application.isEditor && hostMenu) hostMenu.SetActive(state);
        else if (!Application.isEditor && clientMenu) clientMenu.SetActive(state);
    }
}
