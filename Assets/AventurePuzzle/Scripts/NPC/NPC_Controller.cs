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

    public Animator animator;
    public float turnRotSpeed;

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

        if (Vector3.Distance(agent.transform.position, waypoints[currentWaypoint].position) < .5f)
        {
            if (waitForInspect <= timeToInspect)
            {
                waitForInspect += 1 * Time.deltaTime;
                animator.SetBool("Moving", false);
            }
            if (waitForInspect >= timeToInspect)
            {
                animator.SetBool("Moving", true);
                currentWaypoint++;
                waitForInspect = 0;
            }

        }

        if (currentWaypoint >= waypoints.Length)
            currentWaypoint = 0;
        
            //RotateToward();
    }

    void RotateToward()
    {
        Vector3 direction = (waypoints[currentWaypoint].position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnRotSpeed);
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
