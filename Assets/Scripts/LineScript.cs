using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    private LineRenderer line;
    public Transform[] points;

    private void Awake(){
        line = GetComponent<LineRenderer>();
    }
    // count the number of lines for setup
    public void SetUpLine(Transform[] points){
        line.positionCount = points.Length;
        this.points = points;
    }
    // set line positions
    private void Update(){
        for (int i= 0; i < points.Length; i++){
            line.SetPosition(i, points[i].position);
        }
    }
}
