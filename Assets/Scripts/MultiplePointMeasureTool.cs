using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using System;
using TMPro;


public class MultiplePointMeasureTool : MonoBehaviour
{
    private GameObject importModel;
    private float importScale = 1.0f;
    private bool isImported = false;

    [Header("Points")]
    public GameObject measurePoint;
    public GameObject controllerLeft;
    public GameObject controllerRight;
    public GameObject thisObject;

    [Header("Text")]
    public TMP_Text textField;

    [Header("Controller")]
    public XRNode inputSource;

    float totaldDistance;
    float totalarea;
    bool letGo = true;
    List<GameObject> refPoints;
    List<GameObject> refLines;

    public GameObject line;
    public bool closed = false;

    string exportString;
    public GameObject logFile;
    RealLog exportLogScript;

    // create the point and line lists and find the export log
    void Start()
    {
        exportLogScript = logFile.GetComponent<RealLog>();
        refPoints = new List<GameObject>();
        refLines = new List<GameObject>();
        if (thisObject.activeSelf)
        {
            Debug.Log("Is Active");
        }
        textField.text = "Place Measure Points";
    }

    // remove all points
    public void resetpoints()
    {
        foreach (var item in refLines)
        {
            Destroy(item);
        }

        refLines = new List<GameObject>();
        foreach (var item in refPoints)
        {
            Destroy(item);
        }

        refPoints = new List<GameObject>();
        textField.text = "Place Measure Points";
        Measure();
    }
    // where the distance and areas are calculated
    void Measure()
    {
        totaldDistance = 0;
        totalarea = 0.0f;
        float area = 0;
        importScale = 1.0f;
        importModel = GameObject.Find("importModel");
        // have at least two lines before calculating
        if (refLines.Count > 1)
        {
            // sum the total distances
            for (int i = 0; i < refLines.Count - 1; i++)
            {
                float distance = Vector3.Distance(refPoints[i].transform.position, refPoints[i + 1].transform.position);
                totaldDistance += distance;
            }
            // if closed and at least three lines are present, calculate area too
            if (closed && refLines.Count > 2)
            {
                totaldDistance += Vector3.Distance(refPoints[0].transform.position, refPoints[refPoints.Count - 1].transform.position);
                for (int i = 0; i < refPoints.Count; i++)
                {
                    if (i != refPoints.Count - 1)
                    {
                        float mulA = refPoints[i].transform.position.x * refPoints[i + 1].transform.position.z;
                        float mulB = refPoints[i + 1].transform.position.x * refPoints[i].transform.position.z;
                        area += mulA - mulB;
                    }
                    else
                    {
                        float mulA = refPoints[i].transform.position.x * refPoints[0].transform.position.z;
                        float mulB = refPoints[0].transform.position.x * refPoints[i].transform.position.z;
                        area += mulA - mulB;
                    }
                }
                area *= 0.5f;
                totalarea = Mathf.Abs(area);
                // if there is a model present, correct for scaling
                if (isImported)
                {
                    importScale = importModel.transform.localScale.x;
                    textField.text = "Dist, area = " + Mathf.Abs(totaldDistance / importScale).ToString("N2") + ", " + Mathf.Abs(totalarea / Mathf.Pow(importScale, 2)).ToString("N2");
                }
                else
                {
                    textField.text = "Dist, area = " + Mathf.Abs(totaldDistance).ToString("N2") + ", " + Mathf.Abs(totalarea).ToString("N2");
                }
            }
            else
            {
                if (isImported && !closed)
                {
                    importScale = importModel.transform.localScale.x;
                    textField.text = "Distance = " + Mathf.Abs(totaldDistance / importScale).ToString("N2");
                }
                else
                {
                    textField.text = "Distance = " + Mathf.Abs(totaldDistance).ToString("N2");
                }
            }
        }
        // create the export string
        exportString = "MultiDistance, ";
        exportString += Mathf.Abs(totaldDistance / importScale).ToString("N2");
        exportString += ", " + Mathf.Abs(totalarea / Mathf.Pow(importScale, 2)).ToString("N2");
        exportString += ", " + refPoints.Count.ToString("N0");
        for (int i = 0; i < refPoints.Count; i++)
        {
            exportString += ", " + refPoints[i].transform.position.x.ToString("N4");
            exportString += ", " + refPoints[i].transform.position.y.ToString("N4");
            exportString += ", " + refPoints[i].transform.position.z.ToString("N4");
        }
        exportLogScript.outputText = exportString + "\n";
    }

    public void ToggleLoopClosedness()
    {
        closed = !closed;
    }

    public void Imported()
    {
        isImported = true;
    }

    public void exported()
    {
        isImported = false;
    }

    // Update is called once per frame
    void Update()
    {
        Measure();


        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        // instantiate new measuring point to the list
        if (primaryButtonValue & letGo)
        {
            letGo = false;
            
            refLines.Add(Instantiate(line, controllerRight.transform.position, Quaternion.identity));
            refLines[refLines.Count - 1].transform.parent = thisObject.transform;
            refLines[refLines.Count - 1].SetActive(true);

            refPoints.Add(Instantiate(measurePoint, controllerRight.transform.position, Quaternion.identity));
            refPoints[refPoints.Count - 1].transform.parent = thisObject.transform;
            refPoints[refPoints.Count - 1].SetActive(true);
        }

        if(!primaryButtonValue)
        {
            letGo = true;
        }
        // render lines
        if (refLines.Count>1)
        {
            ///int i = 0;
            for (int i=0; i<refPoints.Count-1; i++)
                {
                    LineRenderer linesRender = refLines[i+1].GetComponent<LineRenderer>();
                    linesRender.SetPosition(0,refPoints[i].transform.position);
                    linesRender.SetPosition(1, refPoints[i+1].transform.position);
                    ///i++;
                }
            if (closed)
            {
                LineRenderer linesRender = refLines[0].GetComponent<LineRenderer>();
                linesRender.SetPosition(0, refPoints[0].transform.position);
                linesRender.SetPosition(1, refPoints[refPoints.Count-1].transform.position);
            }
            else
            {
                LineRenderer linesRender = refLines[0].GetComponent<LineRenderer>();
                linesRender.SetPosition(0, refPoints[0].transform.position);
                linesRender.SetPosition(1, refPoints[0].transform.position);
            }
        }
    }
}
