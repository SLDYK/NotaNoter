using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public TextMesh TextMesh;
    public void ResetLine()
    {
        lineRenderer.positionCount = 0;
        TextMesh.anchor = TextAnchor.MiddleLeft;
        TextMesh.text = "";
    }
    public void Line(Vector3 PointA, Vector3 PointB, bool IntBeat, int BeatNum, int Column)
    {
        if (IntBeat)//整数拍横线
        {
            lineRenderer.startWidth = 0.03f;
            lineRenderer.endWidth = 0.03f;
            lineRenderer.startColor = new Color(0, 0.5f, 1f);
            lineRenderer.endColor = new Color(0, 0.5f, 1f);
            TextMesh.text = $" {BeatNum}";
        }
        else if (BeatNum == -1)//竖线
        {
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
            lineRenderer.startColor = new Color(1f, 1f, 0);
            lineRenderer.endColor = new Color(1f, 1f, 0);
            TextMesh.anchor = TextAnchor.UpperCenter;
            if (Column == -1)
            {
                TextMesh.text = "";
            }
            else
            {
                TextMesh.text = $"{Column}";
            }
        }
        else//分拍横线
        {
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
            lineRenderer.startColor = new Color(0, 1f, 0);
            lineRenderer.endColor = new Color(0, 1f, 0);
        }
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, PointA);
        lineRenderer.SetPosition(1, PointB);
        this.transform.position = PointB;
    }
}
