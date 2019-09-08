using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Grapher : MonoBehaviour
{
    [Header("Graph Parameters")]
    private Vector2 offset;

    private float TotalDistance = 0f;
    [Range(0.01f, 1f)] public float DistanceBetweenPoints = 0.1f;

    private Vector3[] points;
    private LineRenderer lineRenderer = null;
    private int arraySize = 0;
    private int tempsCount = 0;

    private Vector3 thispos;
    private int posCount = 0;
    private Vector3 pos;

    private void Start()
    {
        offset = new Vector2(-4f, -4f);
        thispos = this.transform.position;
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.positionCount = posCount;
        lineRenderer.SetPosition(posCount-1, pos);
    }

    public void AddValue(int newTemp)
    {
        float newX = thispos.x + offset.x + TotalDistance;
        float newY = thispos.y + offset.y + Mathf.Lerp(0f, 8f, newTemp/60f);
        pos = new Vector3(newX, newY);
        TotalDistance += DistanceBetweenPoints;
        posCount++;
        
    }
}
