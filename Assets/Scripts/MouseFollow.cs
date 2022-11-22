using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    public float smoothMovement = 5f;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        // Use raycast to get the mouse position in world space
        var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        var layerMask = LayerMask.GetMask("Terrain", "Water", "Environment");
        if (Physics.Raycast(ray, out var hit, layerMask))
        {
            // Smooth the movement to the mouse position
            transform.position = Vector3.Lerp(transform.position, hit.point, smoothMovement * Time.deltaTime);
        }
    }
}
