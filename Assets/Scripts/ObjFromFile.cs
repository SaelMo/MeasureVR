using Dummiesman;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjFromFile : MonoBehaviour
{
    GameObject loadedObject;
    public Material mat;
    // import model in MeasureVRImport folder when activated
    void OnEnable()
    {
        // find folder, if it does not exist create it
        string folderPath = Application.dataPath + @"\MeasureVRImport";
        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }
        DirectoryInfo directory = new DirectoryInfo(folderPath);
        FileInfo[] info = directory.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            Debug.Log("file" + f);
        }
        // get the path of the first file in the MeasureVRImport folder
        string objPath = folderPath + @"\" + @info[0].Name ;

        // if file exists, add to scene and give it collider, material, and add it to a layer
        if (!File.Exists(objPath))
        {
            Debug.LogError("Error" + objPath);
            Debug.LogError("Please set FilePath in ObjFromFile.cs to a valid path.");
            return;
        }
        else
        {
            // import only a single instance of the object
            if (loadedObject != null)
                Destroy(loadedObject);

            loadedObject = new OBJLoader().Load(objPath);
            loadedObject.name = "ImportModel";
            loadedObject.transform.localScale = new Vector3(1, 1, 1);
            loadedObject.layer = 9;

            Transform child = loadedObject.transform.GetChild(0);
            child.GetComponent<MeshRenderer>().material = mat;
            GameObject ChildGameObject = loadedObject.transform.GetChild(0).gameObject;
            ChildGameObject.layer = 9;
            MeshCollider mc = ChildGameObject.AddComponent<MeshCollider>();
        }
    }

}