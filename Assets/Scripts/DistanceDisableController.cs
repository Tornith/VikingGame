using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameObjectDistance
{
    public GameObject gameObject;
    public float distance;
}
public class DistanceDisableController : MonoBehaviour
{
    private readonly List<GameObjectDistance> _objectsToDisable = new();
    
    public void AddObject(GameObject obj, float distance)
    {
        _objectsToDisable.Add(new GameObjectDistance() { gameObject = obj, distance = distance });
    }
    
    private void Update()
    {
        foreach (var obj in _objectsToDisable)
        {
            obj.gameObject.SetActive(Vector3.Distance(transform.position, obj.gameObject.transform.position) < obj.distance);
        }
    }
}
