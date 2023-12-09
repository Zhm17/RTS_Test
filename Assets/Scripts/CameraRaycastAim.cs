using UnityEngine;

public class CameraRaycastAim : MonoBehaviour
{
    public delegate void TargetSpawnedAction(Transform t);
    public static event TargetSpawnedAction OnTargetSpawned;

    private Camera MainCamera;
    [SerializeField]
    private Transform _3dPointer;

    [Header("Target / Spawn")]
    [SerializeField]
    public Transform _targetAreaT;

    private void Start()
    {
        MainCamera = Camera.main;
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
                    ShootClick(hit.point);
                }

            }
        }
    }

    // Spawn destionation points
    private void ShootClick(Vector3 newPos)
    {
        // Spawn cube
        Debug.Log("Shooting");

        if (_targetAreaT)
        {
            _targetAreaT.position = new Vector3(newPos.x, _targetAreaT.position.y, newPos.z);

            if(OnTargetSpawned!=null) 
                OnTargetSpawned(_targetAreaT);
        }
    }
}
