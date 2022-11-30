using UnityEngine;

public class DistanceDisabler : MonoBehaviour
{
    public GameObject player;
    
    public float distance = 150;
    public bool keepPhysics = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<DistanceDisableController>().AddObject(gameObject, distance, keepPhysics);
    }
}
