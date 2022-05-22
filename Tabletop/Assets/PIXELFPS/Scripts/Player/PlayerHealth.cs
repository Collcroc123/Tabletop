using UnityEngine;
using Mirror;
using TMPro;

public class PlayerHealth : NetworkBehaviour
{
    public SpellArrayData spells;
    public TMP_Text healthTxt;
    public int respawnTime = 5;
    private int maxHealth = 100;
    [SyncVar] private int health;
    private NetworkActions netActs;
    private AudioSource audio;
    public AudioClip hurt;

    void Start()
    {
        health = maxHealth;
        netActs = FindObjectOfType<NetworkActions>();
        audio = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        healthTxt.text = health.ToString();
    }
    
    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Spellcast>())
        {
            Spellcast spellcast = other.GetComponent<Spellcast>();
            if (spellcast.player != (int)netId) 
            {
                health -= spellcast.spells.var[spellcast.spellNumber].damage;
                audio.clip = hurt;
                audio.Play();
                if (health <= 0)
                {
                    netActs.RespawnPlayer(gameObject, respawnTime);
                    Invoke(nameof(ResetHealth), respawnTime);// SET HEALTH AFTER RESPAWN!!!
                }
            }
        }
    }

    private void ResetHealth()
    {
        health = maxHealth;
    }
}