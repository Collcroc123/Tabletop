using Mirror;
using UnityEngine;

public class Spellcast : NetworkBehaviour
{
    public SpellArrayData spells;
    [HideInInspector] [SyncVar] public int spellNumber;
    [HideInInspector] [SyncVar] public int player; // Tracks which player cast the spell
    private SpellData spell;
    private Rigidbody rigidBody;
    private SpriteRenderer texture;
    private AudioSource source;
    public GameObject hitAnim; // Sound and particles

    public override void OnStartServer()
    {
        spell = spells.var[spellNumber];
        Invoke(nameof(DestroySelf), spell.duration);
    }

    void Start()
    { // Set velocity for server and client so we don't have to sync the position since both now simulate it.
        texture = GetComponentInChildren<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
        texture.sprite = spell.texture;
        rigidBody.AddForce(transform.forward * spell.force);
        if (spell.castSound.Length == 1)
        {
            source.clip = spell.castSound[0];
            source.pitch = Random.Range(0.9f, 1.1f);
        }
        else source.clip = spell.castSound[Random.Range(0, spell.castSound.Length - 1)];
        source.Play();
    }
    
    [Server]
    void DestroySelf()
    { // Destroy for everyone on the server
        GameObject hit = Instantiate(hitAnim, gameObject.transform.position, gameObject.transform.rotation);
        NetworkServer.Spawn(hit);
        NetworkServer.Destroy(gameObject);
    }
    
    [ServerCallback]
    void OnTriggerEnter(Collider co)
    { // ServerCallback because we don't want a warning if OnTriggerEnter is called on the client
        //Instantiate(hitAnim, gameObject.transform.position, gameObject.transform.rotation);
        if (!co.CompareTag("Spell"))
        {
            if (co.GetComponent<PlayerMovement>() == false || co.GetComponent<PlayerMovement>().netId != player)
            {
                GameObject hit = Instantiate(hitAnim, gameObject.transform.position, gameObject.transform.rotation);
                NetworkServer.Spawn(hit);
                NetworkServer.Destroy(gameObject);
            }
        }
    }
}