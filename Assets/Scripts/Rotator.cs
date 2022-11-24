using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 10f;
    public bool rotateX = true;
    public bool rotateY = true;
    public bool rotateZ = true;

    private void Update()
    {
        var x = rotateX ? speed * Time.deltaTime : 0;
        var y = rotateY ? speed * Time.deltaTime : 0;
        var z = rotateZ ? speed * Time.deltaTime : 0;
        transform.Rotate(x, y, z);
    }
}