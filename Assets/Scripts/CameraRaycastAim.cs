using UnityEngine;

public class CameraRaycastAim : MonoBehaviour
{
    private Camera MainCamera;
    [SerializeField]
    private Transform _3dPointer;

    private void Start()
    {
        MainCamera = Camera.main;
    }

    private void Update()
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
                Debug.DrawRay(transform.position, ray.direction * hit.distance, Color.yellow, 0.01f);
                _3dPointer.position = new Vector3(hit.point.x,_3dPointer.position.y,hit.point.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootClick();
        }
    }

    private void ShootClick()
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
                // Spawn cube
                Debug.Log("Shooting");
            }
        }
    }
}
