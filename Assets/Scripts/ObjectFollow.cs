using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 rotationOffset;
    
    // Lock axis
    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;
    
    public bool copyRotationX = false;
    public bool copyRotationY = false;
    public bool copyRotationZ = false;

    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 currentPosition = transform.position;

        if (lockX)
        {
            targetPosition.x = currentPosition.x;
        }

        if (lockY)
        {
            targetPosition.y = currentPosition.y;
        }

        if (lockZ)
        {
            targetPosition.z = currentPosition.z;
        }

        transform.position = targetPosition;
        
        if (copyRotationX)
        {
            transform.rotation = Quaternion.Euler(target.rotation.eulerAngles.x + rotationOffset.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        
        if (copyRotationY)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y + rotationOffset.y, transform.rotation.eulerAngles.z);
        }
        
        if (copyRotationZ)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, target.rotation.eulerAngles.z + rotationOffset.z);
        }
    }
}
