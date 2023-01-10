using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject hostMenu;
    public GameObject clientMenu;

    void Start()
    {
        if (Application.isEditor) hostMenu.SetActive(true);
        else clientMenu.SetActive(true);
        //#if UNITY_EDITOR
        //#elif UNITY_STANDALONE_WIN
        //#endif
    }

    void Update()
    {
        
    }
}
