using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0, -10);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    private void Update()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
