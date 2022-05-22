using UnityEngine;
using Cursor = UnityEngine.Cursor;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //private bool start = true;
    private bool isActive = true;
    public GameObject sideMenu;
    public GameObject playerUI;
    public GameObject disconnectButton;
    public Animator transition;
    /*
    #region Settings
    [Tooltip("Server name")]
    [SyncVar] public string serverName;
    [Tooltip("Password needed to join server")]
    [SyncVar] public string password;
    [Tooltip("Max number players that can join")]
    [SyncVar] public int maxPlayers = 4;
    [Tooltip("What % the player speed is multiplied")] 
    [SyncVar] [Range(25,300)] public int playerSpeedMultiplier = 100;
    [Tooltip("What % the gravity is multiplied")] 
    [SyncVar] [Range(25, 300)] public int gravityMultiplier = 100;
    [Tooltip("How long after death until the player respawns")] 
    [SyncVar] [Range(0,30)] public int respawnTime = 5;
    #endregion*/

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }
    
    void Start()
    {
        //start = true;
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu") return;
        if (Input.GetKeyDown(KeyCode.Escape))
        { // Show or Hide Menu When Escape is Pressed
            isActive = !isActive;
            sideMenu.SetActive(isActive);
            Cursor.visible = isActive;
            if (isActive) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("HELLO I EXIST!");
        transition.Play("Trans_IN");
        if (scene.name == "Menu")
        {
            //if (!start) Destroy(transform.gameObject);
            Cursor.lockState = CursorLockMode.None;
            isActive = true;
        }
        else isActive = false; //start = false;
        sideMenu.SetActive(isActive);
        playerUI.SetActive(!isActive);
        disconnectButton.SetActive(!isActive);
        Cursor.visible = isActive;
    }
}