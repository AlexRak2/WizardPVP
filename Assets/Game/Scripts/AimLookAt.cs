using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLookAt : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    void Update()
    {
        transform.LookAt(lookAt);
    }
}
