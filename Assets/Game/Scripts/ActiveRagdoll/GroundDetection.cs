using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    ActiveRagdollController controller;
    public Rigidbody rb;

    public float maxSlopeAngle = 45;


    private void Start()
    {
        controller = GetComponentInParent<ActiveRagdollController>();
    }

    public bool grounded = false;
    public bool Colgrounded = false;
    public bool PredictedGrounded = false;

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.contacts[i].normal;

            if (Colgrounded == false)
            {
                Colgrounded = IsFloor(normal);
            }
        }
    }

    private void FixedUpdate()
    {
        if (rb.SweepTest(Vector3.down, out RaycastHit hit, 0.25f))
        {
            if (IsFloor(hit.normal))
            {
                PredictedGrounded = true;
            }
            else
            {
                PredictedGrounded = false;
            }
        }
        else
        {
            PredictedGrounded = false;
        }

        if (PredictedGrounded && Colgrounded)
        {
            grounded = true;
        }
        else if (Colgrounded && !PredictedGrounded)
        {
            grounded = true;
        }
        else if (!Colgrounded && PredictedGrounded)
        {
            grounded = true;
        }
        else if (!Colgrounded && !PredictedGrounded)
        {
            grounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        rb.SweepTest(Vector3.down, out RaycastHit hit, 0.25f);

        Gizmos.DrawLine(hit.point, hit.point + new Vector3(0, hit.distance, 0));
    }
    private void OnCollisionExit(Collision collision)
    {
        Colgrounded = false;
    }

    public bool IsFloor(Vector3 v)
    {
        return Vector3.Angle(Vector3.up, v) < maxSlopeAngle;
    }
}
