using UnityEngine;

public class HabilitarMovimientoMario : MonoBehaviour
{
    ControlMario reset;
    private void Start()
    {
        reset = GetComponent<ControlMario>();
    }
    private void Update()
    {
        if((reset.isGround && !reset.isAttacking) || !reset.isRecovery)
            reset.puedoMoverme = true;
    }
}
