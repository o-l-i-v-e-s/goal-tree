using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{


    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;

    private float distanceToTarget = 10;
    private float cameraHeight;
    private Vector3 previousPosition;

    private void Start()
    {
        if(cam != null)
        {
            if (target != null)
            {
                cameraHeight = cam.transform.position.y;
                Debug.Log("cam position: " + cam.transform.position);
                Debug.Log("target position: " + target.transform.position);
                distanceToTarget = Vector3.Distance(new Vector3(target.transform.position.x, 0, target.transform.position.z), new Vector3(cam.transform.position.x, 0, cam.transform.position.z));
                Debug.Log("DISTANCE TO TARGET: " + distanceToTarget);
            }
            else
            {
                Debug.LogError("Target (around which the camera will rotate) is null on InputManager");
            }

        } else
        {
            Debug.LogError("cam is null on InputManager");
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

            cam.transform.position = new Vector3(target.position.x, cameraHeight, target.position.z);

            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
    }
}
