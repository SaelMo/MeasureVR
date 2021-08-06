using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class HandPresencer : MonoBehaviour
{

    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    private InputDevice targetDevice;
    private GameObject spawnedController;


    // find the VR device at the program start
    void Start()
    {
        TryInitialize();
        Debug.Log(targetDevice);

    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        // activates when VR devices have been found
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            // create models of the VR controllers
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            // if unknown controllers, default to oculus controllers
            else
            {
                Debug.LogError("Did not find corresponding Controller model");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
        }
    }


    // check every frame for the VR device
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
    }
}

