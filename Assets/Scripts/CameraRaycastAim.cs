using UnityEngine;

public class CameraRaycastAim : MonoBehaviour
{
    public delegate void TargetSpawnedAction(Vector3 position);
    public static event TargetSpawnedAction OnTargetSpawned;

    private Camera MainCamera;
    [Header("3D Aim")]
    [SerializeField]
    private Transform _3dPointer;

    [Header("Target / Spawn")]
    [SerializeField]
    public UnitTarget _targetAreaT;

    [SerializeField]
    private UnitTarget _teleportAreaT;

    public enum STATE
    {
        TARGET = 1,
        TELEPORT = 2
    }

    [Header("State")]
    public STATE State = STATE.TARGET;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        MainCamera = Camera.main;

        _targetAreaT?.gameObject.SetActive(false);
        _teleportAreaT?.gameObject.SetActive(false);

        State = STATE.TARGET;
    }

    private void Update()
    {
        PointNClick();
    }

    private void PointNClick()
    {
        RaycastHit hit;
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Ground"))
            {
                // Ray Debug
                Debug.DrawRay(transform.position, ray.direction * hit.distance, Color.yellow, 0.01f);

                //Move 
                if (_3dPointer)
                    _3dPointer.position = new Vector3(hit.point.x, _3dPointer.position.y, hit.point.z);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Shoot(hit.point);
                }

            }
        }
    }

    /// <summary>
    /// Spawn destination points
    /// </summary>
    /// <param name="newPos"></param>
    private void Shoot(Vector3 newPos)
    {
        // Spawn cube
        //Debug.Log("Shooting");

        switch (State)
        {
            case STATE.TARGET:
               
                //Shoot TargetAreaT transform
                if (_targetAreaT)
                {
                    ActiveNSetPoint(_targetAreaT, newPos);

                    if (OnTargetSpawned != null)
                        OnTargetSpawned(_targetAreaT.transform.position);
                }

                // change to TELEPORT
                State = STATE.TELEPORT;
                break;
            
            case STATE.TELEPORT:
                
                //Shoot TeleportAreaT transform 
                if (_teleportAreaT)
                {
                    ActiveNSetPoint(_teleportAreaT, newPos);
                }

                //change to TARGET
                State = STATE.TARGET;
                break;
            
            default:
                //do nothing
                break;
        }
    }

    /// <summary>
    /// Show and set position
    /// </summary>
    /// <param name="unitTarget"></param>
    /// <param name="pos"></param>
    private void ActiveNSetPoint(UnitTarget unitTarget, Vector3 pos)
    {
        unitTarget.Show();

        unitTarget.transform.position = new Vector3(pos.x, 
                                                    unitTarget.transform.position.y, 
                                                    pos.z);
    }
}
