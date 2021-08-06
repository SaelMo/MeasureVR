using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.IO;

public class LogData : MonoBehaviour
{
    [Header("Log")]
    public TMP_Text logText;
    [Header("Export log")]
    public TMP_Text exportLog;
    private string exportText = "Time of measurement: " + DateTime.Now.ToString("dd-MM-yyyy h:mm:ss tt") + "\n";

    private string textholder;
    RealLog exportLogScript;
    // get the log for the exported data (real log)
    void Start()
    {
        exportLogScript = GetComponent<RealLog>();
    }
    // when log data is executed, take both a display measurement and an export measurement
    public void Logdata()
    {
        textholder = logText.text+ "\n";
        textholder += exportLog.text + "\n";
        exportLog.text = textholder; 
        exportText += exportLogScript.outputText;
    }

    // when export is executed, save all data that has been logged until now
    public void Export()
    {
        string folderPath = Application.dataPath + @"\MeasureVRExport";
        string filePath = folderPath + "\\Measurements.txt";
        // see if there is an appropiate folder, if not, create one
        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }
        if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "Export Log of MeasureVR\n");
            }
    // export data from the real log
    Debug.Log(exportText);
    File.AppendAllText(filePath, exportText);
    }
}
