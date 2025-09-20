using UnityEngine;

public enum State
{
    Patrol,
    Chase,
    Attack
}

public class SimpleFSM : MonoBehaviour
{
    private State currentState;
}
