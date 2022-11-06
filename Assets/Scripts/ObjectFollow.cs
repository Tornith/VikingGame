using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    
    // Lock axis
    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;

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
    }
}
