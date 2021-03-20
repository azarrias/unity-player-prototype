using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private NavMeshAgent agent;
    public float PatrolFrequency = 10f;
    private const float TICK_FREQUENCY = 0.5f; 
    public Transform[] waypoints;

    private Transform player;

    private int waypointIndex;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        waypointIndex = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, TICK_FREQUENCY);
        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", 0, PatrolFrequency);
        }
    }

    void Patrol()
    {
        waypointIndex = waypointIndex == waypoints.Length - 1 ? 0 : waypointIndex + 1;
    }

    void Tick()
    {
        agent.destination = waypoints[waypointIndex].position;
    }
}
