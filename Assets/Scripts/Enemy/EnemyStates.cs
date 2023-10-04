using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStates 
{
    protected EnemyController enemy;
    
    public EnemyStates(EnemyController _enemy)
    {
        this.enemy = _enemy;
    }

    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStateExit();
}
