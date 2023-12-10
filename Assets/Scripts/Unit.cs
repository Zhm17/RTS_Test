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

    private void GetTarget(Vector3 position)
    {
        if(position == null)
            return;

        agent.destination = position;
    }

}
