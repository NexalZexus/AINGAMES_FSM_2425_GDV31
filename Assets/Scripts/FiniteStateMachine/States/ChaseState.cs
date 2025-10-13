using UnityEngine;

public class ChaseState : FSMState
{
    public override StateID StateId => StateID.Chase;
    private Transform playerPos;

    public ChaseState(EnemyTankController enemyTankController)
    {
        _enemyTankController = enemyTankController;
        playerPos = _enemyTankController.Player;
    }

    public override void CheckTransition(Transform agent, Transform player)
    {
        if (Vector3.Distance(agent.position, player.position) > _enemyTankController.ChaseDistance)
        {
            _enemyTankController.PerformTransition(TransitionID.LostPlayer);
        }
        if (Vector3.Distance(agent.position, player.position) <= _enemyTankController.AttackDistance)
        {
            _enemyTankController.PerformTransition(TransitionID.ReachPlayer);
        }
    }

    public override void RunState(Transform agent, Transform player)
    {
        _enemyTankController.MoveToTarget(playerPos);
    }
}
