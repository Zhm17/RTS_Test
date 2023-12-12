using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraZoom : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField]
    private float _zoom;
    [SerializeField]
    private float _zoomMultiplier = 4f;
    
    [Header("Min / Max")]
    [SerializeField]
    private float _minZoom = 10f;
    [SerializeField]
    private float _maxZoom = 15f;
    [SerializeField] 

    [Header("Velocity")]
    private float _velocity = 0f;
    [SerializeField]
    private float _smoothTime = 0.25f;

    [SerializeField] 
    private CinemachineVirtualCamera _virtualCamera => 
        GetComponent<CinemachineVirtualCamera>();

    // Start is called before the first frame update
    void Start()
    {
        _zoom = _virtualCamera.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        _zoom -= scroll * _zoomMultiplier;
        _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);

        _virtualCamera.m_Lens.OrthographicSize = 
            Mathf.SmoothDamp(_virtualCamera.m_Lens.OrthographicSize,
                                _zoom, 
                                ref _velocity,
                                _smoothTime);
    }
}
