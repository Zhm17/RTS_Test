using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    public Transform targetPosition;
    NavMeshAgent agent => GetComponent<NavMeshAgent>();

    // Start is called before the first frame update
    void Start()
    {
        agent.destination = targetPosition!.position;
    }

}
