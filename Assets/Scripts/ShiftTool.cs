using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ShiftTool : MonoBehaviour
{
    private InputDevice right;
    private GameObject importModel;
    private Vector3 shift;
    List<InputDevice> devicesRightHand = new List<InputDevice>();
    InputDeviceCharacteristics rightControllers = InputDeviceCharacteristics.Right;
    void Start()
    {
        shift = new Vector3(0f, 0.01f, 0f);
    }

    void Update()
    {

        InputDevices.GetDevicesWithCharacteristics(rightControllers, devicesRightHand);
        if (devicesRightHand.Count > 0)
        {
            right = devicesRightHand[0];
        }

        right.TryGetFeatureValue(CommonUsages.grip, out float rightGrip);
        right.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryFaceButton);
        right.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryFaceButton);

        // check for right grip, then look for the imported model
        if (rightGrip > 0.9f)
        {
            importModel = GameObject.Find("ImportModel");
            if (importModel != null)
            {
                // translate downward
                if ((primaryFaceButton == true) && (secondaryFaceButton == false))
                {
                    importModel.transform.localPosition -= shift;
                }
                // translate upward
                else if ((primaryFaceButton == false) && (secondaryFaceButton == true))
                {
                    importModel.transform.localPosition += shift;
                }
            }
        }
    }
}
