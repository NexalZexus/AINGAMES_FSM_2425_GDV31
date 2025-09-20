using UnityEngine;

public class SimpleFSM : MonoBehaviour
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

    private State currentState;
    private Transform currentTarget;

    private void Start()
    {
        currentState = State.Patrol;
        RandomizeWaypointTarget();
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                DoPatrol();
                break;
            case State.Chase:
                DoChase();
                break;
            case State.Attack:
                DoAttack();
                break;
        }
    }

    private void SetCurrentTarget(Transform target)
    {
        currentTarget = target;
    }

    private void RandomizeWaypointTarget()
    {
        // Randomize a value from the array.
        // Random.Range when int, max is exclusive so we can directly use the length of array
        int randomIndex = Random.Range(0, waypoints.Length);

        // Ensure that the next randomized waypoint is unique
        while (waypoints[randomIndex] == currentTarget)
        {
            randomIndex = Random.Range(0, waypoints.Length);
        }
        SetCurrentTarget(waypoints[randomIndex]);
    }

    private void DoAttack()
    {
        
    }

    private void DoChase()
    {
        
    }

    private void DoPatrol()
    {
        // Enemy will move to the target waypoint
        Vector3 targetDirection = currentTarget.position - transform.position;
        // Get the rotation that faces the targetDirection
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        // Do the actual rotation by making the enemy look towards the targetRotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        // Since it's already rotated, just make it move forward
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);


        // Keep track of the distance
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
        if (distanceToTarget < waypointDistance)
        {
            RandomizeWaypointTarget();
        }
    }
}
