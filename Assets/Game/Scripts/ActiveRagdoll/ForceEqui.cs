using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceEqui : MonoBehaviour
{
    public float force;
    public ForceMode mode;
    public Rigidbody rb;

    public float up = 1;

    public void FixedUpdate()
    {
        rb.AddForce(force * Vector3.up*up, mode);
    }
}

