using UnityEngine;
using Mirror;

[AddComponentMenu("")]
public class NetManager : NetworkManager
{
    //public bool client;
    
    public void Quit()
    {
        Application.Quit();
    }
}
