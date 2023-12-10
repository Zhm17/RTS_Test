using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    NavMeshAgent agent => GetComponent<NavMeshAgent>();

    #region EVENTS SUSCRIPTIONS
    private void OnEnable()
    {
        CameraRaycastAim.OnTargetSpawned += GetTarget;
        CameraRaycastAim.OnTeleportSpawned += GetTeleport;
    }

    private void OnDisable()
    {
        CameraRaycastAim.OnTargetSpawned -= GetTarget;
        CameraRaycastAim.OnTeleportSpawned -= GetTeleport;
    }
    #endregion

    [SerializeField]
    private Vector3 _targetPosition;
    [SerializeField]
    private Vector3 _teleportPosition;

    private void GetTarget(Vector3 pos)
    {
        if(pos == null)
            return;
        _targetPosition = pos;
        agent.SetDestination(pos);
    }

    public void GetTeleport(Vector3 pos)
    {
        if (pos == null)
            return;

        _teleportPosition = pos;       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            //Debug.Log("Teleport position: " + _teleportPosition);
            transform.position = (_teleportPosition != Vector3.zero) ?
                                // true
                                new Vector3(_teleportPosition.x, 
                                transform.position.y,
                                _teleportPosition.z) :
                                //false
                                new Vector3(_targetPosition.x,
                                transform.position.y,
                                _targetPosition.z);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Teleport"))
        {
            transform.position = new Vector3(_teleportPosition.x,
                                                transform.position.y,
                                                _teleportPosition.z);
        }
    }

    private void ResetTargetPosition(bool active)
    {
        _targetPosition = Vector3.zero;
    }

    private void ResetTeleportPosition(bool active)
    {
        _teleportPosition = Vector3.zero;
    }

}
