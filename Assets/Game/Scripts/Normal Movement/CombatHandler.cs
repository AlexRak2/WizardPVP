using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    IKController ikController;

    private void Start()
    {
        ikController = GetComponent<IKController>();
    }

}
