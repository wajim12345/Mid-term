using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : TurretState
{
    public TurretIdleState(TurretController _turret) : base(_turret)
    {

    }
    public override void OnStateEnter()
    {
        Debug.Log("Turrent is in idle mode");
        turret.LaserColor(Color.green);
    }

    public override void OnStateExit()
    {
        Debug.Log("Turret is exiting idle mode");
    }

    public override void OnStateUpdate()
    {
        if (Physics.Raycast(turret.turretEye.position, Vector3.forward, out RaycastHit hit, turret.checkPlayerDistance))
        {
            turret.SetLaserLength(new Vector3(turret.laserOrigin.position.x, turret.laserOrigin.position.y, turret.laserOrigin.position.z + hit.distance));
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player Found!");
                turret.ChangeState(new TurretAttackState(turret));
                
                
            }
            else
            {
                return;
            }
        }
        else
        {
            turret.ResetLaserLength();
            
        }
    }
}
