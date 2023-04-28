using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    ActiveRagdollController controller;

    public float maxSlopeAngle = 45;


    private void Start()
    {
        controller = GetComponentInParent<ActiveRagdollController>();
    }
    public bool grounded = false;
    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.contacts[i].normal;

            if (grounded == false)
            {
                grounded = IsFloor(normal);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    public bool IsFloor(Vector3 v)
    {
        return Vector3.Angle(Vector3.up, v) < maxSlopeAngle;
    }
}
