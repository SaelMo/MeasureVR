using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AngleMeasureTool : MonoBehaviour
{
    [Header("Points")]
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject controllerLeft;
    public GameObject controllerRight;

    [Header("Text")]
    public TMP_Text textFieldAtHandCanvas;
    public GameObject logFile;

    string exportString;
    float distance;
    float relativeAngle;
    RealLog exportLogScript;

    void Start()
    {
        exportLogScript = logFile.GetComponent<RealLog>();
    }

    // return the spheres to the user their hands
    public void SetAtHands()
    {
        pointA.transform.position = controllerLeft.transform.position;
        pointC.transform.position = controllerRight.transform.position;
        pointB.transform.position = (pointA.transform.position+ pointC.transform.position)/2;
    }

    // calculate the angles of the triangle
    void Measure()
    {
        // the string displayed to the user during runtime
        float AngleR = Vector3.Angle(pointA.transform.position - pointB.transform.position, pointC.transform.position - pointB.transform.position);
        float AngleG = Vector3.Angle(pointB.transform.position - pointC.transform.position, pointA.transform.position - pointC.transform.position);
        float AngleB = Vector3.Angle(pointB.transform.position - pointA.transform.position, pointC.transform.position - pointA.transform.position);
        textFieldAtHandCanvas.text = "R, G, B = " + AngleR.ToString("N0")+ "°, " + AngleG.ToString("N0") + "°, " + AngleB.ToString("N0") + "°";

        // preparing string for the export file
        exportString = "TriAngle, ";
        exportString += AngleR.ToString("N2");
        exportString += ", " + AngleG.ToString("N2");
        exportString += ", " + AngleB.ToString("N2");
        exportString += ", " + pointA.transform.position.x.ToString("N4");
        exportString += ", " + pointA.transform.position.y.ToString("N4");
        exportString += ", " + pointA.transform.position.z.ToString("N4");
        exportString += ", " + pointB.transform.position.x.ToString("N4");
        exportString += ", " + pointB.transform.position.y.ToString("N4");
        exportString += ", " + pointB.transform.position.z.ToString("N4");
        exportString += ", " + pointC.transform.position.x.ToString("N4");
        exportString += ", " + pointC.transform.position.y.ToString("N4");
        exportString += ", " + pointC.transform.position.z.ToString("N4") + "\n" ;
        exportLogScript.outputText = exportString;
    }

    void Update()
    {
        Measure();
    }
}
