using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectToInstantiate;

    [SerializeField]
    private float objectScale;


    [SerializeField]
    private Camera arCamera;

    private GameObject spawnedObject;

    // objectSpawned needs to be static, so it can be visible and used in other classes/scripts
    public static bool objectSpawned;

    private ARPlaneManager arPlaneMan;
    private ARRaycastManager arRaycastMan;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Vector2 touchOne;
    private Vector2 touchTwo;
    private float distCurrent;
    private float distPrevious;
    bool firstPinch = true;

    private Vector3 touchOffset;
    bool isDragging;
    private float touchDistance;

    private void Start()
    {
        objectSpawned = false;
        arPlaneMan = GetComponent<ARPlaneManager>();
        arRaycastMan = GetComponent<ARRaycastManager>();
    }

    void Update()
    {

        // Raycast only if user touches the screen -> show model or move it
        if (Input.touchCount == 1 && !objectSpawned)
        {
            if (arRaycastMan.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                // Place model on the first plane that user hit
                Pose hitPose = hits[0].pose;

                // Spawn gameobject on selected position
                gameObjectToInstantiate.transform.localScale = gameObjectToInstantiate.transform.localScale * objectScale;

                spawnedObject = Instantiate(gameObjectToInstantiate, hitPose.position, hitPose.rotation);
                objectSpawned = true;

                arPlaneMan.enabled = false;
                arRaycastMan.enabled = false;

            }
        }

        // If gameobject is already spawned -> DRAG it to touch position
        if (Input.touchCount == 1 && objectSpawned)
        {
            Touch touch = Input.GetTouch(0);

            if (arRaycastMan.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    Ray ray = arCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;
                    if (Physics.Raycast(ray, out hitObject))
                    {
                        spawnedObject.transform.position = hitObject.point;

                        Pose hitPose = hits[0].pose;

                        if (hitObject.transform.position == spawnedObject.transform.position)
                        {
                            spawnedObject.transform.position = hitPose.position;
                        }
                    }
                }
            }
        }

    }

    // Toggle visibility of AR planes 
    public void OnTogglePlaneVisCLick()
    {
        arPlaneMan.enabled = !arPlaneMan.enabled;
        if (arPlaneMan.enabled)
        {
            SetALlPlanesActive(true);
        }
        else
        {
            SetALlPlanesActive(false);
        }
    }

    // Select all scanned AR planes
    private void SetALlPlanesActive(bool value)
    {
        foreach (var plane in arPlaneMan.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}





