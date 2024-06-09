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
    public bool randomizeSpeed = false;
    public Vector2 randomSpeedRange = new Vector2(1, 4);
    public bool randomizeInspect = false;
    public Vector2 randomInspectRange = new Vector2(1, 6);

    void Start()
    {
        TryGetComponent(out NavMeshAgent _agent);
        agent = _agent;

        agent.velocity = Vector3.zero;
        if (randomizeInspect) { timeToInspect = Random.Range(randomInspectRange.x, randomInspectRange.y); }
        if (randomizeSpeed) { wanderSpeed = Random.Range(randomSpeedRange.x, randomSpeedRange.y); }
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
        if(agent == null)
        {
            TryGetComponent(out NavMeshAgent _agent);
            agent = _agent;
        }

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
