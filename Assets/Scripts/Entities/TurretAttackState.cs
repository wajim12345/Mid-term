using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TurretAttackState : TurretState
{
    float distanceToPlayer;
    Health playerHealth;
    float damagePerSec = 20f;

    public TurretAttackState(TurretController _turret) : base(_turret)
    {
        playerHealth = _turret.player.GetComponent<Health>();
    }
    public override void OnStateEnter()
    {
        Debug.Log("Turret is in Attack mode");
        turret.LaserColor(Color.red);
    }

    public override void OnStateExit()
    {
        Debug.Log("Turrent is exiting attack mode");
    }

    public override void OnStateUpdate()
    {
        if (turret.player != null)
        {
            if (Physics.Raycast(turret.turretEye.position, turret.transform.forward, out RaycastHit hit, turret.checkPlayerDistance))
            {
                turret.SetLaserLength(new Vector3(turret.laserOrigin.position.x, turret.laserOrigin.position.y, turret.laserOrigin.position.z + hit.distance));
                if (hit.transform.CompareTag("Player"))
                {
                    Attack();
                }
                else
                {
                    turret.ResetLaserLength();
                    turret.ChangeState(new TurretIdleState(turret));
                }
            }
            else
            {
                turret.ResetLaserLength();
                turret.ChangeState(new TurretIdleState(turret));
            }
        }
    }
    void Attack()
    {
        if (playerHealth != null)
        {
            playerHealth.DeductHealth(damagePerSec * Time.deltaTime);
        }
    }
}

