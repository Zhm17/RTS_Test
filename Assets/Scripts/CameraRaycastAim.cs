using UnityEngine;

public class CameraRaycastAim : MonoBehaviour
{
    public delegate void TargetPointSpawnedAction(Vector3 position);
    public static event TargetPointSpawnedAction OnTargetSpawned;
    public static event TargetPointSpawnedAction OnTeleportSpawned;

    private Camera MainCamera;
    [Header("3D Aim")]
    [SerializeField]
    private Transform _3dPointer;

    [Header("Target / Spawn")]
    [SerializeField]
    public UnitTarget _targetAreaT;

    [SerializeField]
    private UnitTarget _teleportAreaT;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        MainCamera = Camera.main;

        _targetAreaT?.gameObject.SetActive(false);
        _teleportAreaT?.gameObject.SetActive(false);

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
                    ShootTarget(hit.point);

                if (Input.GetKeyDown(KeyCode.Mouse1))
                    ShootTeleport(hit.point);
            }
        }
    }

    /// <summary>
    /// Spawn destination points
    /// </summary>
    /// <param name="newPos"></param>
    private void ShootTarget(Vector3 newPos)
    {
        // Spawn Target
        //Debug.Log("Shooting Target");
               
        //Shoot TargetAreaT transform
        if (_targetAreaT)
        {
            ActiveNSetPoint(_targetAreaT, newPos);

            if (OnTargetSpawned != null)
                OnTargetSpawned(_targetAreaT.transform.position);
        }
    }

    private void ShootTeleport(Vector3 newPos)
    {
        // Spawn Teleport
        //Debug.Log("Shooting Target");

        //Shoot TeleportAreaT transform 
        if (_teleportAreaT)
        {
            ActiveNSetPoint(_teleportAreaT, newPos);

            if (_teleportAreaT != null)
                OnTeleportSpawned(_teleportAreaT.transform.position);
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
