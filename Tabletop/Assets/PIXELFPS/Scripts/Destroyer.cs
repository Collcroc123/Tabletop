using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float time = 3f;
    void Start()
    {
        Invoke(nameof(Dest), time);
    }

    public void Dest()
    {
        Destroy(gameObject);
    }
}