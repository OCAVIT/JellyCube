using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float rotationSpeed = 100f;
    public float zoomSpeed = 2f;
    public float minDistance = 2f;
    public float maxDistance = 10f;

    private float currentAngle = 0f;
    private float currentDistance;

    void Start()
    {
        currentDistance = offset.magnitude;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        float horizontalInput = Input.GetAxis("Horizontal");
        currentAngle += horizontalInput * rotationSpeed * Time.deltaTime;

        float zoomInput = Input.GetAxis("Vertical");
        currentDistance -= zoomInput * zoomSpeed * Time.deltaTime;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 rotatedOffset = rotation * offset.normalized * currentDistance;

        Vector3 desiredPosition = target.position + rotatedOffset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}