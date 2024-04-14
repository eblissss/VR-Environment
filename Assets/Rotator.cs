using UnityEngine;

public class Rotator : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.right, 2f, Space.World);
    }
}
