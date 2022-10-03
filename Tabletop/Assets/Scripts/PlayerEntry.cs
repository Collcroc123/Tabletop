using UnityEngine;

public class PlayerEntry : MonoBehaviour
{
    public GameObject parentClient;

    private void Update()
    {
        if (parentClient == null)
        {
            Destroy(transform.gameObject);
        }
    }
}
