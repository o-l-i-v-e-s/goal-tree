using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject container;
    private Camera cam;
    [SerializeField] private Transform target;

    private float distanceToTarget = 10;
    private float containerHeight;
    private Vector3 previousPosition;

    private void Start()
    {
        if(container != null)
        {
            cam = container.GetComponentInChildren<Camera>();
            Debug.Log(cam);
            if (target != null)
            {
                containerHeight = container.transform.position.y;
                Debug.Log("container position: " + container.transform.position);
                Debug.Log("target position: " + target.transform.position);
                distanceToTarget = Vector3.Distance(new Vector3(target.transform.position.x, 0, target.transform.position.z), new Vector3(container.transform.position.x, 0, container.transform.position.z));
                Debug.Log("DISTANCE TO TARGET: " + distanceToTarget);
            }
            else
            {
                Debug.LogError("Target (around which the camera container will rotate) is null on InputManager");
            }

        } else
        {
            Debug.LogError("container is null on InputManager");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180;

            container.transform.position = new Vector3(target.position.x, containerHeight, target.position.z);

            container.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

            container.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
    }
}
