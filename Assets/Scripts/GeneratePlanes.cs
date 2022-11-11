using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlanes : MonoBehaviour
{
    public GameObject plane;
    public int renderDistance = 12;
    public int planeSize = 20;
    public Vector3 planeOffset = new Vector3(0, 0, 0);
    
    private void Start(){
        // Generate the planes
        for (var x = -renderDistance; x < renderDistance; x++){
            for (var z = -renderDistance; z < renderDistance; z++){
                var newPlane = Instantiate(plane, new Vector3(x * planeSize, 0, z * planeSize) + planeOffset, Quaternion.identity);
                // Set the parent of the plane to this object, without changing its position
                newPlane.transform.SetParent(transform, false);
            }
        }
    }
}
