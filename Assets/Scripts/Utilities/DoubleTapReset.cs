using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapReset : MonoBehaviour
{

    private Vector3 initialCameraPosition;
    private Quaternion initialModelRotation;

    private float lastTapTime;
    private const float DoubleTapDelay = 0.3f;

    private void Start()
    {
        initialCameraPosition = Camera.main.transform.position;
        initialModelRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.touchCount != 1)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Ended)
            return;

        if (Time.time - lastTapTime < DoubleTapDelay)
        {
            ResetView();
        }

        lastTapTime = Time.time;
    }

    private void ResetView()
    {
        Camera.main.transform.position = initialCameraPosition;
        transform.rotation = initialModelRotation;
    }
}
