
using UnityEngine;


[CreateAssetMenu(menuName = "New Item/Wand")]

public class WandItem : ScriptableObject
{
    public byte id;
    public string name;
    public string description;
    public float damage;
    public GameObject wandPrefab;

    public SpellItem[] spells;
}
