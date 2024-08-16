using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using Assets.Scripts.Player;
using UnityEngine.AI;
using ModestTree;

public class TestAgentScript : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    void Start()
    {
        player = FindFirstObjectByType<Target>().gameObject;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z));
    }
}
