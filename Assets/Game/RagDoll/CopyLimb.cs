using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyLimb : MonoBehaviour
{
    [SerializeField] private Transform targetLimb;
    private ConfigurableJoint configurableJoint;

    Quaternion targetInitialRotation;

    private void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
        targetInitialRotation = targetLimb.localRotation;
    }

    private void FixedUpdate()
    {
        configurableJoint.targetRotation = CopyRotation();
    }

    private Quaternion CopyRotation() 
    {
        return Quaternion.Inverse(targetLimb.localRotation) * targetInitialRotation;
    }
}
