using UnityEngine;

[CreateAssetMenu(menuName = "Datas/SpellData")]
public class SpellData : ScriptableObject
{
    [Tooltip("The base spell prefab")]
    public GameObject prefab;
    [Tooltip("How much damage the spell does when colliding with a player")]
    public int damage;
    [Tooltip("How many times per second the spell can be cast")]
    public float rate;
    [Tooltip("The physical speed of the cast spell")]
    public float force;
    [Tooltip("How long the cast spell lasts before disappearing")]
    public float duration;
    [Tooltip("How much mana the spell costs to cast")]
    public float cost;
    [Tooltip("Determines if the spell bounces off walls or disappears on impact")]
    public bool canBounce;
    [Tooltip("The icon of the spell in the book")]
    public Sprite icon;
    [Tooltip("The texture of the cast spell")]
    public Sprite texture;
    [Tooltip("Sounds that play when casting the spell")]
    public AudioClip[] castSound;
    /*
    void Fire()
    {
        GameObject projectile = Instantiate(prefab, castPoint.transform.position, castPoint.transform.rotation);
        projectile.GetComponent<Spellcast>().spell = this;
        projectile.GetComponent<Spellcast>().spell.player = (int)netId;
        NetworkServer.Spawn(projectile);
    }*/
}