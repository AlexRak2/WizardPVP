using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClientInitialize : NetworkBehaviour
{
    [SerializeField] private GameObject camFollow;
    [SerializeField] private GameObject cameraObj;
    CameraInitialize cameraInstance;

    ActiveRagdollController activeRagdollController;
    CombatHandler combatHandler;

    private void Awake()
    {
        activeRagdollController = GetComponent<ActiveRagdollController>();
        combatHandler = GetComponent<CombatHandler>();

    }
    private void Start()
    {
        if (!isOwned) 
        {
            Destroy(GetComponent<PlayerInput>());
            Destroy(GetComponent<CharacterInput>());

            return;
        }

        cameraInstance = Instantiate(cameraObj).GetComponent<CameraInitialize>();
        cameraInstance.SetUp(camFollow.transform);

        activeRagdollController.lockOnTarget = cameraInstance.lookAt.transform;
        combatHandler.aimPoint = cameraInstance.lookAt.transform;
    }
}
