using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ScaleTool : MonoBehaviour
{
    private InputDevice left;
    private GameObject importModel;
    private Vector3 scaleFactor;
    List<InputDevice> devicesLeftHand = new List<InputDevice>();
    InputDeviceCharacteristics leftcontrollers = InputDeviceCharacteristics.Left;

    void Start()
    {
        scaleFactor = new Vector3(0.01f, 0.01f, 0.01f);
    }

    // check every frame if the user wants to scale or not
    void Update()
    {

        InputDevices.GetDevicesWithCharacteristics(leftcontrollers, devicesLeftHand);
        if (devicesLeftHand.Count > 0)
        {
            left = devicesLeftHand[0];
        }

        left.TryGetFeatureValue(CommonUsages.grip, out float leftGrip);
        left.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryFaceButton);
        left.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryFaceButton);

        // check for left grip, then look for the imported model
        if (leftGrip > 0.9f)
        {
            importModel = GameObject.Find("ImportModel");
            if (importModel != null)
            {
                // decrease scale
                if ((primaryFaceButton == true) && (secondaryFaceButton == false))
                {
                    importModel.transform.localScale -= scaleFactor;
                }
                // increase scale
                else if ((primaryFaceButton == false) && (secondaryFaceButton == true))
                {
                    importModel.transform.localScale += scaleFactor;
                }
            }
        }
    }
}
