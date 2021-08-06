using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class LocomotionController : MonoBehaviour
{
    public XRController LeftTeleportRay;
    public XRController RightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;
    private InputDevice targetDevice;
    public Material mat;
    public bool fade;

    // Update is called once per frame
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
    void Update()
    {

        if (fade)
        {
            targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
            Color c = mat.color;
            c.a = triggerValue;
            mat.color = c;
        }


        if(LeftTeleportRay)
        {
            LeftTeleportRay.gameObject.SetActive(CheckIfActivated(LeftTeleportRay));
        }
        if (RightTeleportRay)
        {
            RightTeleportRay.gameObject.SetActive(CheckIfActivated(RightTeleportRay));
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
