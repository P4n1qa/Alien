﻿using UnityEngine;

namespace Root.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
    
        [SerializeField] private float _smoothSpeed = 0.125f; 
        [SerializeField] private Vector3 _offset; 

        private void LateUpdate()
        {
            if (_target == null) return;
            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}