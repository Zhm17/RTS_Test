using UnityEngine;
using System.Collections.Generic;

public class CameraRaycastAim : MonoBehaviour
{

    private Camera MainCamera;
    [Header("3D Aim")]
    [SerializeField]
    private Transform _3dPointer;

    [Header("Target / Spawn")]
    [SerializeField]
    public List<UnitTarget> TargetAreaList;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        MainCamera = Camera.main;

        foreach (UnitTarget target in TargetAreaList)
            target.gameObject.SetActive(false);

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
            }
        }
    }

    /// <summary>
    /// Spawn destination points
    /// </summary>
    /// <param name="newPos"></param>
    private void ShootTarget(Vector3 newPos)
    {
        //Debug.Log("Shooting Target");
               
        //Shoot TargetAreaT transform
        foreach (UnitTarget target in TargetAreaList)
        {
            // find the first element inactive
            if (!target.isActiveAndEnabled)
            {
                // active and set position
                ActiveNSetPoint(target, newPos);
                return;
            }
        }
    }

    /// <summary>
    /// Show and set position
    /// </summary>
    /// <param name="unitTarget"></param>
    /// <param name="pos"></param>
    private void ActiveNSetPoint(UnitTarget unitTarget, Vector3 pos)
    {
        unitTarget.Show(pos);
    }
}
