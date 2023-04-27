using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKLookAt : MonoBehaviour
{
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

    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        animator.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
        animator.SetLookAtPosition(lookAtPosition.position);
    }
}
