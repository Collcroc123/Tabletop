using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour
{
    public GameObject outline;

    public void Select()
    {
        outline.transform.SetParent(transform, false);
    }
}
