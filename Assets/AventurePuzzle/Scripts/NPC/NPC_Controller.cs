using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Controller : MonoBehaviour
{
    NavMeshAgent agent;

    [Header("Settings")]
    public float wanderSpeed;
    public float timeToInspect;
    public Transform[] waypoints;

    float waitForInspect;
    int currentWaypoint;

    public bool inAstralMode;

    void Start()
    {
        TryGetComponent(out NavMeshAgent _agent);
        agent = _agent;

        agent.velocity = Vector3.zero;
    }

    void Update()
    {
        if(!inAstralMode) 
            GoToWaypoints();
    }

    void GoToWaypoints()
    {
        agent.speed = wanderSpeed;
        agent.SetDestination(waypoints[currentWaypoint].position);
        
        if (agent.isPathStale)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
        }

        if (Vector3.Distance(agent.transform.position, waypoints[currentWaypoint].position) < 2f)
        {
            if (waitForInspect <= timeToInspect)
            {
                waitForInspect += 1 * Time.deltaTime;
            }
            if (waitForInspect >= timeToInspect)
            {
                currentWaypoint++;
                waitForInspect = 0;
            }
        }

        if (currentWaypoint >= waypoints.Length)
            currentWaypoint = 0;
        
    }

    public void SwitchAstralState(bool astral)
    {
        if (astral)
        {
            inAstralMode = true;
            agent.enabled = false;
        }
        else
        {
            inAstralMode = false;
            agent.enabled = true;
        }
    }
}
