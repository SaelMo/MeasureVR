using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyModel : MonoBehaviour
{
    // on activation check if model exist, if it exists destroy it
    public void destroy()
    {
        if (GameObject.Find("ImportModel") != null)
        {
            GameObject importModel = GameObject.Find("ImportModel");
            Debug.Log("Import object found");
            Destroy(importModel);
        }
        else
        {
            Debug.Log("Import object not found");
        }
    }
}
