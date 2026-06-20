
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 0.01f;

    [SerializeField] private float zoomInDistance = 2f;
    public Transform target;
    private float initialDistance;

    bool canTrack;
    public void Initialize()
    {

        if (target == null)
        {
            Debug.LogError("Target is not assigned!");
            enabled = false;
            return;
        }

        initialDistance = Vector3.Distance(transform.position, target.position);
        canTrack = true;
    }

    private void Update()
    {
        if (!canTrack) return;

        if (Input.touchCount != 2)
            return;

        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

        float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
        float currentMagnitude = (touch0.position - touch1.position).magnitude;

        float difference = currentMagnitude - prevMagnitude;

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * difference * zoomSpeed;
        float currentDistance = Vector3.Distance(transform.position, target.position);
        float maxDistance = initialDistance;
        float minDistance = Mathf.Max(0.1f, initialDistance - zoomInDistance);

        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        transform.position = target.position - direction * currentDistance;
    }

}