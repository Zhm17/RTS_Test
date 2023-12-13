using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    private NavMeshAgent _agent => GetComponent<NavMeshAgent>();

    #region EVENTS SUSCRIPTIONS
    private void OnEnable()
    {
        UnitTarget.OnSpawn += SetTarget;
    }

    private void OnDisable()
    {
        UnitTarget.OnSpawn -= SetTarget;
    }
    #endregion

    [SerializeField]
    private UnitTarget _currentTarget;
    public UnitTarget CurrentTarget
    {
        get { return _currentTarget; }
        private set { _currentTarget = value; }
    }

    [SerializeField]
    private UnitTarget _currentTeleport;
    public UnitTarget CurrentTeleport
    {
        get { return _currentTeleport; }
        private set { _currentTeleport = value; }
    }
    


    private void SetTarget(UnitTarget target, bool active)
    {
        if (target == null)
            return;

        // Add
        if (active)
        {
            AddTarget(target);
        } 

        //Remove
        else
        {
            RemoveTarget(target);
        }
    }

    private void AddTarget(UnitTarget target)
    {
        if (!CurrentTarget)
        {
            CurrentTarget = target;
            _agent.destination = CurrentTarget.transform.position;
        }
        else
        {
            CurrentTeleport = target;
        }
    }

    private void RemoveTarget(UnitTarget target)
    {
        if(CurrentTarget
            && CurrentTarget == target)
        {
            CurrentTarget = null;
            _agent.ResetPath();
        }

        else if(CurrentTeleport 
            && CurrentTeleport == target)
        {
            CurrentTeleport = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Compare Tag and target
        if (other.CompareTag("Target") 
            && CurrentTarget == other.GetComponent<UnitTarget>())
        {
            //Debug.Log("Inside Target");

            // Validate
            Teleport();
            _agent.ResetPath();

            CurrentTarget = null;
        }
    }

    private void Teleport()
    {
        if (CurrentTeleport == null) 
            return;

        _agent.enabled = false;
        
        transform.position = CurrentTeleport.transform.position;

        _agent.enabled = true;
    }

}
