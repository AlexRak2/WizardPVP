using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private CombatHandler combatHandler;

    private void Start()
    {
        combatHandler = GetComponentInParent<CombatHandler>();
    }
    public void StartSpell() 
    {
        combatHandler.UseSpell();
    }
}
