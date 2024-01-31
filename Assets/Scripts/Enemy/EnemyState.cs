using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyController enemy;
    public EnemyState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}
