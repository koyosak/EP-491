
using UnityEngine;

public class EnemyAliveState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.IsAlive = true;

    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        
        enemy.WanderingAI.Wander();
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
     
        enemy.IsAlive = false;
    }
}
