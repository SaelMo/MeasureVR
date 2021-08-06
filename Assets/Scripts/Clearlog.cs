using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Clearlog : MonoBehaviour
{
    [Header("Log")]
    public TMP_Text textField;
    RealLog ExportLogScript;

    void Start()
    {
        ExportLogScript = GetComponent<RealLog>();
    }
    // empty the string
    public void clearlog()
    {
        textField.text = " ";
        ExportLogScript.outputText = "";
    }
}
