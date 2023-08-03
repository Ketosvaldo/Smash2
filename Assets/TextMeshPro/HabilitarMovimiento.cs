using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilitarMovimiento : MonoBehaviour
{
    ControlGoku1 reset;
    private void Start()
    {
        reset = GetComponent<ControlGoku1>();
    }
    private void Update()
    {
        if ((reset.isGround && !reset.isAttacking) || !reset.isRecovery)
            reset.puedoMoverme = true;
    }
}
