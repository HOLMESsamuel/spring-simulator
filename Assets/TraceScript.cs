using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TraceScript : MonoBehaviour
{

    public LineRenderer lineRenderer;

    public int traceSize;
    public float traceStep;
    public bool display;

    public List<Vector3> tracePoints;

    // Start is called before the first frame update
    void Start()
    {
        traceSize = 100;
        traceStep = 0.1F;
        display = true;
    }

    void appendTrace(Vector2 tracePoint)
    {
        tracePoints.Add(tracePoint);
        if (tracePoints.Count > traceSize)
        {
            tracePoints.RemoveAt(0);
        }
        for (int i = 0; i < tracePoints.Count; i++)
        {
            tracePoints[i] += new Vector3(-traceStep, 0, 0);
        }
    }

    void DrawTrace()
    {
        lineRenderer.startWidth = 0.05F;
        lineRenderer.endWidth = 0.05F;
        lineRenderer.positionCount = tracePoints.Count;
        lineRenderer.SetPositions(tracePoints.ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        appendTrace(FindObjectOfType<SpringScript>().GetPosition());
        if(display)
        {
            DrawTrace();
        }
        
    }
}
