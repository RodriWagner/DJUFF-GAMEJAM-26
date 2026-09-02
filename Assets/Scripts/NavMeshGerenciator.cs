using System.Drawing;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGerenciator : MonoBehaviour
{
    [SerializeField] private GameObject point;
    private NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        agent.SetDestination(point.transform.position);
    }

}
