using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class OneDistMeasureTool : MonoBehaviour
{

    private GameObject importModel;
    private bool isImported= false;

    [Header("Points")]
    public GameObject pointA;
    public GameObject pointB;
    public GameObject controllerLeft;
    public GameObject controllerRight;

    [Header("Text")]
    public TMP_Text textFieldAtHandCanvas;
    public GameObject logFile;

    float distance;
    string exportString;
    RealLog exportLogScript;

    void Start()
    {
        exportLogScript = logFile.GetComponent<RealLog>();
    }

    // reset position of spheres to hands
    public void SetAtHands()
    {
        pointA.transform.position = controllerLeft.transform.position;
        pointB.transform.position = controllerRight.transform.position;
    }

    public void imported()
    {
        isImported  = true;
    }

    public void exported()
    {
        isImported = false;
    }

    void Measure()
    {
        float distance = Vector3.Distance(pointA.transform.position, pointB.transform.position);
        // if model is imported, multiply distance by scale
        if (isImported)
        {
            importModel = GameObject.Find("ImportModel");
            distance /= importModel.transform.localScale.x;
        }

        // measurement for display
        textFieldAtHandCanvas.text = "Distance = " + distance.ToString("N2");
        // measurement for export
        exportString = "SingleDistance, ";
        exportString += distance.ToString("N2");
        exportString += ", " + pointA.transform.position.x.ToString("N4");
        exportString += ", " + pointA.transform.position.y.ToString("N4");
        exportString += ", " + pointA.transform.position.z.ToString("N4");
        exportString += ", " + pointB.transform.position.x.ToString("N4");
        exportString += ", " + pointB.transform.position.y.ToString("N4");
        exportString += ", " + pointB.transform.position.z.ToString("N4") + "\n";
        exportLogScript.outputText = exportString;

    }

    void Update()
    {
        Measure();
    }
}
