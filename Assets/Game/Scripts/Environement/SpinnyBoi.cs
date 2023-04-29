using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyBoi : MonoBehaviour
{
    public Vector3 angularVel;
    public Rigidbody rb;

    private void FixedUpdate()
    {
        rb.angularVelocity = angularVel;
    }
}
