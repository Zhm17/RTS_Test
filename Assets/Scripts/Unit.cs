using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    public Transform targetPosition;
    NavMeshAgent agent => GetComponent<NavMeshAgent>();

    #region EVENTS SUSCRIPTIONS
    private void OnEnable()
    {
        CameraRaycastAim.OnTargetSpawned += GetTarget;
    }

    private void OnDisable()
    {
        CameraRaycastAim.OnTargetSpawned -= GetTarget;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if(targetPosition) 
            agent.SetDestination(targetPosition.position);
    }

    private void GetTarget(Transform t)
    {
        if(t == null)
            return;

        agent.destination = t.position;
    }

}
