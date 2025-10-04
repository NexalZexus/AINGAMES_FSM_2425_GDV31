using UnityEngine;

public class ChaseState : FSMState
{
    public override StateID StateId => StateID.Chase;
    private EnemyTankController _enemyTankController;

    public ChaseState(EnemyTankController enemyTankController)
    {

    }

    public override void CheckTransition(Transform agent, Transform player)
    {

    }

    public override void RunState(Transform agent, Transform player)
    {

    }
}
