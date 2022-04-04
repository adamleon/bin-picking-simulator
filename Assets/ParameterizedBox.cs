using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterizedBox : MonoBehaviour
{
    [Range(30,200)]
    public int width = 50;
    [Range(30,200)]
    public int length = 50;

    Transform bottom, south, north, west, east, boundingBox;
    
    [ContextMenu("Construct Box")]
    void Awake()
    {
        bottom = transform.Find("Bottom");
        north = transform.Find("North");
        south = transform.Find("South");
        west = transform.Find("West");
        east = transform.Find("East");
        boundingBox = transform.Find("Bounding Box");
        
        ConstructBox();
    }

    public float boxLength { get {return length;} }
    public float boxWidth { get {return width;} }
    public float boxThickness { get {return 1f;} }
    public float boxHeight { get {return 20f;} }
    public float boundingBoxHeight { get {return 500f;} }

    public Vector3 RandomPositionWithin() {
        return new Vector3(
            Random.Range(-boxLength/2f, boxLength/2f),
            Random.Range(boxHeight,boundingBoxHeight),
            Random.Range(-boxWidth/2f, boxWidth/2f)
        );
    }

    void ConstructBox() {

        bottom.localScale = new Vector3(boxLength, boxThickness, boxWidth);
        bottom.localPosition = new Vector3(0,-boxThickness, 0);

        north.localScale = new Vector3(boxThickness, boxHeight+boxThickness+0.01f, boxWidth);
        north.localPosition = new Vector3(boxLength/2f+boxThickness/2f, boxHeight/2f-boxThickness-0.01f, 0);
        
        south.localScale = north.localScale;
        south.localPosition = new Vector3(
            -north.localPosition.x,
            north.localPosition.y,
            north.localPosition.z);

        west.localScale = new Vector3(boxLength+2*boxThickness, boxHeight+boxThickness+0.01f, boxThickness);
        west.localPosition = new Vector3(0, boxHeight/2f-boxThickness-0.01f, boxWidth/2f+boxThickness/2f);
        
        east.localScale = west.localScale;
        east.localPosition = new Vector3(
            west.localPosition.x,
            west.localPosition.y,
            -west.localPosition.z);

        
        BoxCollider[] bounds = boundingBox.GetComponents<BoxCollider>();
        // bottom
        bounds[0].size = new Vector3(boxLength,boxThickness,boxWidth);
        bounds[0].center = new Vector3(0,-boxThickness,0);
        
        // top
        bounds[1].size = new Vector3(boxLength,boxThickness,boxWidth);
        bounds[1].center = new Vector3(0,boundingBoxHeight,0);

        // north
        bounds[2].size = new Vector3(boxThickness,boundingBoxHeight+boxThickness,boxWidth);
        bounds[2].center = new Vector3(boxLength/2f+boxThickness/2f,boundingBoxHeight/2f-boxThickness,0);

        // south
        bounds[3].size = bounds[2].size;
        bounds[3].center = new Vector3(
            -bounds[2].center.x,
            bounds[2].center.y,
            bounds[2].center.z
        );

        // west
        bounds[4].size = new Vector3(boxLength+2*boxThickness,boundingBoxHeight+boxThickness,boxThickness);
        bounds[4].center = new Vector3(0,boundingBoxHeight/2f-boxThickness,boxWidth/2f+boxThickness/2f);

        // east
        bounds[5].size = bounds[4].size;
        bounds[5].center = new Vector3(
            bounds[4].center.x,
            bounds[4].center.y,
            -bounds[4].center.z
        );
    }
}
