using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraInitialize : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public GameObject lookAt;

    public void SetUp(Transform obj) 
    {
        virtualCamera.LookAt = obj;
        virtualCamera.Follow = obj;
    }
}
