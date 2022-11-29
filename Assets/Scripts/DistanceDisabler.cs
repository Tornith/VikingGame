using UnityEngine;

public class DistanceDisabler : MonoBehaviour
{
    public GameObject player;
    
    public float distance = 150;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<DistanceDisableController>().AddObject(gameObject, distance);
    }
}
