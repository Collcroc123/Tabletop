using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool enableOrDisable = true;
    public bool inverted = false;
    public GameObject hostMenu;
    public GameObject clientMenu;

    [Header("UI")]
    public GameObject newsMenu;
    public GameObject settingsMenu;
    private bool toggled;

    void Start()
    {
        if (Application.isEditor && hostMenu) 
        {
            if (!inverted) hostMenu.SetActive(enableOrDisable);
            else clientMenu.SetActive(enableOrDisable);
        }
        else if (!Application.isEditor && clientMenu) 
        {
            if (!inverted) clientMenu.SetActive(enableOrDisable);
            else hostMenu.SetActive(enableOrDisable);
        }
    }

    public void ToggleSettings()
    {
        toggled = !toggled;
        settingsMenu.SetActive(toggled);
        newsMenu.SetActive(!toggled);
    }
}