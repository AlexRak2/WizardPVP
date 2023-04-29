using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Item/Spell")]
public class SpellItem : ScriptableObject
{
    public enum SpellType { Projectile, Unknown }
    public SpellType Type = SpellType.Projectile;


    public string name;
    public GameObject spellPrefab;
}
