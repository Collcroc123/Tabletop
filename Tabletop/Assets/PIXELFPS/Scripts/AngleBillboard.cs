using System;
using UnityEngine;

public class AngleBillboard : MonoBehaviour
{
    public Sprite[] spriteSheet;
    private Camera mainCam;
    private SpriteRenderer sprite;
    private float angle;

    void Start()
    {
        mainCam = Camera.main;
        sprite = GetComponent<SpriteRenderer>();
    }
    
    void LateUpdate()
    {
        if (mainCam != null)
        {
            transform.LookAt(mainCam.transform);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
            if (spriteSheet.Length == 8)
            {
                Vector3 direction = mainCam.transform.position - transform.parent.position;
                angle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg;
                if (transform.parent.eulerAngles.y - angle < 180) angle -= transform.parent.eulerAngles.y;
                else angle -= transform.parent.eulerAngles.y - 360;
                int index = (int)MathF.Round(angle/45);
                if (index < 0) index += 8;
                sprite.sprite = spriteSheet[index];
            }
        }
        else mainCam = Camera.main;
    }
}