using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientStats : NetworkBehaviour, IDamageable
{
    [SyncVar] public int health = 100;
    [SyncVar] public bool isDead = false;


    [SerializeField] private ConfigurableJoint[] joints;



    [Command(requiresAuthority = false)]
    public void Damage(int damage)
    {
        if (isDead) return;

        health -= damage;

        if (health <= 0) 
        {
            isDead = true;
            RPC_Death();

            Invoke(nameof(Rpc_Respawn), 5f);
        }
    }

    [ClientRpc]
    public void RPC_Death()
    {
        SetRagDoll(true);

    }

    private void SetRagDoll(bool value)
    {
        GetComponentInChildren<Animator>().enabled = false;

        foreach (ConfigurableJoint joint in joints)
        {
            JointDrive jointDrive = new JointDrive();
            jointDrive.positionSpring = 0;
            joint.angularXDrive = jointDrive;
            joint.angularYZDrive = jointDrive;

            joint.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    [ClientRpc]
    public void Rpc_Respawn() 
    {
        transform.position = MyNetworkManager.singleton.GetStartPosition().position;

    }
}
