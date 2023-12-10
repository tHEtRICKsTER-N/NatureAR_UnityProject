using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlantsPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _plants;

    ARRaycastManager _raycastManager;
    ARPlaneManager _planeManager;

    List<ARRaycastHit> _hitList = new List<ARRaycastHit> ();

    bool planeGeneration = true;

    private void Start()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
        _planeManager = GetComponent<ARPlaneManager>();
    }

    private void Update()
    {
        if(Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(_raycastManager.Raycast(Input.mousePosition,_hitList,UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose pose = _hitList[0].pose;
                var plant = Instantiate(_plants[Random.Range(0,_plants.Length-1)]);
                plant.transform.position = pose.position;
            }
        }
    }

    public void TogglePlaneGeneration()
    {
        //ON
        if (planeGeneration)
        {
            _planeManager.enabled = true;
            planeGeneration = false;
            foreach (var plane in _planeManager.trackables)
            {
                plane.gameObject.SetActive(true);
            }
            return;
        }

        //OFF
        foreach(var plane in _planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
        _planeManager.enabled = false;
        planeGeneration = true;
    }



}
