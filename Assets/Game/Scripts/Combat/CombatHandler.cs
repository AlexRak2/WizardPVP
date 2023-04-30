using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : NetworkBehaviour
{
    [Header("Wand")]
    [SerializeField] private WandItem[] allWands;
    [SerializeField] private WandItem defaultWand;
    [SerializeField] private WandItem currentWand;
    [SerializeField] private GameObject rightHand;
    public Transform aimPoint;
    [SyncVar(hook = nameof(OnWandIndexChange))] byte currentWandIndex;
    Wand currentWandInstance;

    [SerializeField] private Animator anim;

    IKController ikController;
    ClientStats clientStats;

    private void Start()
    {
        if (!isOwned) return;

        ikController = GetComponent<IKController>();
        clientStats = GetComponent<ClientStats>();

        Cmd_EquipWand(defaultWand.id);
    }

    private void Update()
    {
        if (!isOwned) return;
        if (clientStats.isDead) return;

        if (Input.GetMouseButtonDown(0)) 
        {
            anim.SetBool("Attack", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Attack", false);
        }
    }

    public void UseSpell() 
    {
        Vector3 lookAt = aimPoint.position;
        Cmd_UseSpell(lookAt);
    }

    [Command]
    public void Cmd_UseSpell(Vector3 lookAt)
    {
        Rpc_UseSpell(lookAt);
    }

    [ClientRpc]
    private void Rpc_UseSpell(Vector3 lookAt) 
    {
        GameObject spell = Instantiate(currentWandInstance.wandItem.spells[0].spellPrefab, currentWandInstance.spellPoint.transform.position, Quaternion.identity);
        spell.transform.LookAt(lookAt);
        Destroy(spell, 10f);
    } 


    #region Equip Wand
    [Command]
    public void Cmd_EquipWand(byte id) 
    {
        currentWandIndex = id;
    }

    private void OnWandIndexChange(byte _, byte index)
    {
        //destroying old wand
        if (currentWand) 
        {
            Destroy(currentWandInstance.gameObject);
        }


        //equipping new want
        currentWand = allWands[index - 1];
        currentWandInstance = Instantiate(currentWand.wandPrefab, rightHand.transform).GetComponent<Wand>();
    }
    #endregion
}
