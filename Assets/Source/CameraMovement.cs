using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private float cameraSpeed = 1.0f;
    
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _followTarget.position + _cameraOffset, Time.deltaTime * cameraSpeed);
    }
}
