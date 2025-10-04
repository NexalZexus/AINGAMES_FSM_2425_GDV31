// used in SimpleFSM
public enum State
{
    Patrol,
    Chase,
    Attack
}


// used in AdvancedFSM

public enum StateID
{
    None,
    Patrol,
    Chase,
    Attack
}

public enum TransitionID
{
    None,
    SawPlayer,
    LostPlayer,
    ReachPlayer
}