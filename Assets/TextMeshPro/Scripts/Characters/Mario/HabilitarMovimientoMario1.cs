using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilitarMovimientoMario1 : MonoBehaviour
{
    ControlMario1 reset;
    private void Start()
    {
        reset = GetComponent<ControlMario1>();
    }
    private void Update()
    {
        if((reset.isGround && !reset.isAttacking) || !reset.isRecovery)
            reset.puedoMoverme = true;
    }
}
