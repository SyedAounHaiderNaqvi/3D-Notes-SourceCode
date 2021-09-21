using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class NoteData
{
    public float[] stripColor;
    public float[] worldSpacePosition;
    public string titleText;
    public string descriptionText;
    string name;

    public NoteData (DragMessages msgs)
	{
        stripColor = new float[3];
        stripColor[0] = msgs.colorStrip.material.color.r;
        stripColor[1] = msgs.colorStrip.material.color.g;
        stripColor[2] = msgs.colorStrip.material.color.b;

        worldSpacePosition = new float[3];
        worldSpacePosition[0] = msgs.transform.position.x;
        worldSpacePosition[1] = msgs.transform.position.y;
        worldSpacePosition[2] = msgs.transform.position.z;

        titleText = msgs.transform.Find("title").GetComponent<TextMeshPro>().text;
        descriptionText = msgs.transform.Find("body").GetComponent<TextMeshPro>().text;

        name = msgs.gameObject.name;
    }
}
