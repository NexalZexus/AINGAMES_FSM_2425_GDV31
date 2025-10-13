using UnityEngine;

public class EnemyTankController : AdvancedFiniteStateMachine
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;

    [Header("Health")]
    [SerializeField]
    private int curHealth;
    [SerializeField]
    private int maxHealth;

    [Header("AI Variables")]
    [Tooltip("How close to the player is considered as a chasing range")]
    [SerializeField]
    private float chaseDistance;

    [SerializeField]
    private float attackDistance;

    [Tooltip("How close to the target waypoint before moving to the next waypoint")]
    [SerializeField]
    private float waypointDistance;

    [Tooltip("Points that the tank will randomly move towards during patrol state")]
    [SerializeField]
    private Transform[] waypoints;

    [Tooltip("Reference to the player tank")]
    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject Explosion;

    [SerializeField]
    private HealthBar healthBar;

    private Transform turret;
    private Transform bulletSpawnPoint;
    private float turretRotSpeed = 10.0f;

    public float ChaseDistance => chaseDistance;
    public float AttackDistance => attackDistance;
    public float WaypointDistance => waypointDistance;
    public Transform Turret => turret;
    public Transform BulletSpawnPoint => bulletSpawnPoint;
    public Transform Player => player;
    public GameObject Bullet => bullet;
    protected override void Initialize()
    {
        curHealth = maxHealth;
        turret = gameObject.transform.GetChild(0).transform;
        bulletSpawnPoint = turret.GetChild(0).transform;
        healthBar.updateHealthBar(maxHealth, curHealth);

        // Define the actual FSM and possible states the enemy tank controller can be
        PatrolState patrolState = new PatrolState(this, waypoints);
        // Define the transition rules
        patrolState.AddTransition(TransitionID.SawPlayer, StateID.Chase);

        ChaseState chaseState = new ChaseState(this);
        chaseState.AddTransition(TransitionID.ReachPlayer, StateID.Attack);
        chaseState.AddTransition(TransitionID.LostPlayer, StateID.Patrol);

        AttackState attackState = new AttackState(this);
        attackState.AddTransition(TransitionID.ChasePlayer, StateID.Chase);

        AddState(patrolState);
        AddState(chaseState);
        AddState(attackState);

        Debug.Log("Added states");
    }

    protected override void UpdateStateMachine()
    {
        healthBar.updateHealthBar(maxHealth, curHealth);
        CurrentState.RunState(this.transform, player);
        CurrentState.CheckTransition(this.transform, player);
        dead();

    }

    public void MoveToTarget(Transform currentTarget)
    {
        // Enemy will move to the target waypoint
        Vector3 targetDirection = currentTarget.position - transform.position;
        // Get the rotation that faces the targetDirection
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        // Do the actual rotation by making the enemy look towards the targetRotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        // Since it's already rotated, just make it move forward
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }
     public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }
    private void dead()
    {
        if (curHealth <= 0)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
