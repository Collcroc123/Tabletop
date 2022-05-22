using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("Hovering", true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("Hovering", false);
    }
}