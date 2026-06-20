using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.2f;

    private void Update()
    {
        if (Input.touchCount != 1)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            float rotationY = -touch.deltaPosition.x * rotationSpeed;
            transform.Rotate(Vector3.up, rotationY, Space.World);
        }
    }
}
