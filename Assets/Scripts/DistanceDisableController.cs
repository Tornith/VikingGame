using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class GameObjectDistance
{
    public GameObject gameObject;
    public float distance;
    public bool keepPhysics = false;
}
public class DistanceDisableController : MonoBehaviour
{
    private readonly List<GameObjectDistance> _objectsToDisable = new();
    
    public void AddObject(GameObject obj, float distance, bool keepPhysics = false)
    {
        _objectsToDisable.Add(new GameObjectDistance
        {
            gameObject = obj,
            distance = distance,
            keepPhysics = keepPhysics
        });
    }
    
    private void Update()
    {
        foreach (var obj in _objectsToDisable)
        {
            if (obj.gameObject == null)
            {
                _objectsToDisable.Remove(obj);
                break;
            }

            var active = Vector3.Distance(transform.position, obj.gameObject.transform.position) < obj.distance;
            if (!obj.keepPhysics && active == obj.gameObject.activeSelf) continue;
            if (obj.keepPhysics)
            {
                // Disable/Enable agent and enemy scripts
                obj.gameObject.GetComponent<NavMeshAgent>().enabled = active;
                obj.gameObject.GetComponent<Enemy.Enemy>().enabled = active;
            }
            else
            {
                obj.gameObject.SetActive(active);
            }
        }
    }
}
