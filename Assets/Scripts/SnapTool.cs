using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class SnapTool : MonoBehaviour
{
    public LayerMask mask;
    public GameObject snappingReticle;

    private InputDevice right;
    private Vector3 hitLocation = Vector3.zero;
    private bool toggled = false;
    private XRGrabInteractable grabbable;
    private GameObject lastHeld;
    List<InputDevice> devicesRight = new List<InputDevice>();
    InputDeviceCharacteristics rightControllers = InputDeviceCharacteristics.Right;

    public void toggle()
    {
        toggled = !toggled;
    }

    // release the last held object
    public void released()
    {   
        if (lastHeld != null)
        {
            lastHeld.transform.position = snappingReticle.transform.position;
            lastHeld = null;
        }
        snappingReticle.SetActive(false);
    }

    void Update()
    {
        // find the object currently being held
        var interactor = GetComponent<XRBaseInteractor>();
        grabbable = interactor.selectTarget as XRGrabInteractable;
        InputDevices.GetDevicesWithCharacteristics(rightControllers, devicesRight);

        if (devicesRight.Count > 0)
        {
            right = devicesRight[0];
        }

        right.TryGetFeatureValue(CommonUsages.grip, out float RightGrip);
        // when toggled, shoot a ray that interacts with objects in a chosen layer
        if (toggled)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo;
            // update position of reticle based on location where the ray intercepts the object
            if (Physics.Raycast(ray, out hitInfo, 100, mask) && grabbable != null)
            {
                lastHeld = grabbable.gameObject;
                hitLocation = hitInfo.point;
                snappingReticle.SetActive(true);
                snappingReticle.transform.position = hitLocation;               
            }
        }
    }
}
