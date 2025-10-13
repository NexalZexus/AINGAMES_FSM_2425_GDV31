using UnityEngine;

public class AttackState : FSMState
{
    public override StateID StateId => StateID.Attack;
    private Transform playerPos;
    private Transform turret;
    private Transform bulletSpwnPnt;
    private float shootRate;

    public AttackState(EnemyTankController enemyTankController)
    {
        _enemyTankController = enemyTankController;
        playerPos = _enemyTankController.Player;
        turret = _enemyTankController.Turret;
        bulletSpwnPnt = _enemyTankController.BulletSpawnPoint;
    }

    public override void CheckTransition(Transform agent, Transform player)
    {
        if(Vector3.Distance(agent.position, player.position) > _enemyTankController.AttackDistance)
        {
            _enemyTankController.PerformTransition(TransitionID.ChasePlayer);
        }
    }

    public override void RunState(Transform agent, Transform player)
    {
        turret.transform.LookAt(playerPos);
        shootRate += Time.deltaTime;
        if(shootRate > 0.5f)
        {
            shootRate = 0.0f;
            Shoot();
        }

    }

    private void Shoot ()
    {
        GameObject.Instantiate(_enemyTankController.Bullet, bulletSpwnPnt.position, bulletSpwnPnt.rotation);
    }
}
