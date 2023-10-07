using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private NavMeshAgent agent;
    private Vector3 destination;
    private float timeout = 5f;
    private float startTime;

    public MoveCommand(NavMeshAgent _agent, Vector3 _destination)
    {
        agent = _agent;
        destination = _destination;
    }
    public override bool isComplete => ReachDestination(); 

    public override void Execute()
    {
        startTime = Time.time;
        agent.SetDestination(destination);
    }

    bool ReachDestination()
    {
        if ((agent.remainingDistance < 0.1f) || (Time.time - startTime >= timeout))
        {
            return true;
        }

        return false;
            
    }
}
