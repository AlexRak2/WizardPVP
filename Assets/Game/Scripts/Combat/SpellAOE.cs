using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAOE : MonoBehaviour
{
    public float radius;

    public float Force;
    
    public List<IDamageable> clientHits = new List<IDamageable>();
    private void Start()
    {
        DamageArea();
    }
    public void DamageArea() 
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("RagDoll"))
            {
                IDamageable client = hitCollider.GetComponentInParent<IDamageable>();

                if (clientHits.Contains(client)) continue;
                client.Damage(15);

                clientHits.Add(client);
            }
            if (hitCollider.gameObject.GetComponent<Rigidbody>())
            {
                hitCollider.gameObject.GetComponent<Rigidbody>().AddExplosionForce(Force, transform.position, radius,0, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
