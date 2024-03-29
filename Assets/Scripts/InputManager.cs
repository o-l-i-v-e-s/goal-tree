using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject container;
    private Camera cam;
    [SerializeField] private Transform target;

    [SerializeField] private float distanceToTarget = 10;
    private float containerHeight;
    private Vector3 previousPosition;
    private GameObject clickedGameObject;
    private bool clickedOnGameObject = false;

    private void Start()
    {
        if(container != null)
        {
            cam = container.GetComponentInChildren<Camera>();
            Debug.Log(cam);
            if (target != null)
            {
                containerHeight = container.transform.position.y;
                distanceToTarget = Vector3.Distance(new Vector3(target.transform.position.x, 0, target.transform.position.z), new Vector3(container.transform.position.x, 0, container.transform.position.z));
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

    void HandleRotateCamera()
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

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            clickedOnGameObject = true;
            // save previousPosition for use in rotating screen
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    clickedGameObject = raycastHit.transform.gameObject;
                }
            }
            if (clickedGameObject != null && clickedGameObject.CompareTag("Leaf"))
            {
                Leaf leaf = clickedGameObject.GetComponent<Leaf>();
                leaf.HandleClick();
            }
        }
        else if (clickedOnGameObject && Input.GetMouseButton(0))
        {
            // when clicking anywhere but the leaf
            if (clickedGameObject == null || !clickedGameObject.CompareTag("Leaf"))
            {
                HandleRotateCamera();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // clear out the clicked game object
            clickedGameObject = null;
            clickedOnGameObject = false;
        }
    }
}
