using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{

    [SerializeField]Animator anim;

    [Range(0f, 1f)]
    [SerializeField] private float weight = 0.0f;
    [Range(0f, 1f)]
    [SerializeField] private float bodyWeight = 0.2f;
    [Range(0f, 1f)]
    [SerializeField] private float headWeight = 0.5f;
    [Range(0f, 1f)]
    [SerializeField] private float eyesWeight = 0.7f;
    [Range(0f, 1f)]
    [SerializeField] private float clampWeight = 0.5f;
    [SerializeField] private Transform lookAtPosition;

    [Header("Right Hand IK")]
    [Range(0, 1)] public float rightHandWeight;
    public Transform rightHandObj = null;
    public Transform rightHandHint = null;

    [Header("Left Hand IK")]
    [Range(0, 1)] public float leftHandWeight;
    public Transform leftHandObj = null;
    public Transform leftHandHint = null;

    private void OnAnimatorIK()
    {
        if (anim)
        {
            if (lookAtPosition) 
            {
                anim.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
                anim.SetLookAtPosition(lookAtPosition.position);
            }


            #region RIGHT HAND IK

            if (rightHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
                anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            }

            if (rightHandHint != null)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
                anim.SetIKHintPosition(AvatarIKHint.RightElbow, rightHandHint.position);
            }

            #endregion

            #region LEFT HAND IK

            if (leftHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            }

            if (leftHandHint != null)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
                anim.SetIKHintPosition(AvatarIKHint.LeftElbow, leftHandHint.position);
            }

            #endregion
        }
    }
}