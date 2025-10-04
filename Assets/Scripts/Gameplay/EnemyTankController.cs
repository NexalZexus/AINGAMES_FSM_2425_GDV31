using UnityEngine;

public class EnemyTankController : AdvancedFiniteStateMachine
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;

    [Header("AI Variables")]
    [Tooltip("How close to the player is considered as a chasing range")]
    [SerializeField]
    private float chaseDistance;

    [Tooltip("How close to the target waypoint before moving to the next waypoint")]
    [SerializeField]
    private float waypointDistance;

    [Tooltip("Points that the tank will randomly move towards during patrol state")]
    [SerializeField]
    private Transform[] waypoints;

    [Tooltip("Reference to the player tank")]
    [SerializeField]
    private Transform player;


    public float ChaseDistance => chaseDistance;
    public float WaypointDistance => waypointDistance;

    protected override void Initialize()
    {
        // Define the actual FSM and possible states the enemy tank controller can be
        PatrolState patrolState = new PatrolState(this, waypoints);
        // Define the transition rules
        patrolState.AddTransition(TransitionID.SawPlayer, StateID.Chase);

        ChaseState chaseState = new ChaseState(this);
        chaseState.AddTransition(TransitionID.ReachPlayer, StateID.Attack);
        chaseState.AddTransition(TransitionID.LostPlayer, StateID.Patrol);

        AddState(patrolState);
        AddState(chaseState);

        Debug.Log("Added states");
    }

    protected override void UpdateStateMachine()
    {
        CurrentState.RunState(this.transform, player);
        CurrentState.CheckTransition(this.transform, player);
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
}
