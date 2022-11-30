
using System;
using UnityEngine;

[Serializable]
public class GameObjectTransform
{
    public GameObject gameObject;
    public Vector3 position;
}

public class SpawnObjects : MonoBehaviour
{
    public GameObjectTransform[] objects;
    
    private void Spawn()
    {
        foreach (var obj in objects)
        {
            Instantiate(obj.gameObject, transform.position + obj.position, Quaternion.identity);
        }
    }
}
