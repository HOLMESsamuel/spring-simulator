using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class SpringScript : MonoBehaviour
{
    public float diskRadius;
    public float gravity;
    public Vector2 boundsSize;
    public float collisionDamping;
    Vector2 position;
    Vector2 velocity;
    Mesh mesh;
    public Vector3[] polygonPoints;
    public int[] polygonTriangles;
    public Vector3 origine;

    public List<Vector3> stringPoints;

    public int numberOfTurns;
    public float stringWidth;
    public float maxLength;
    public float stringLength;

    public LineRenderer lineRenderer;

    void Start()
    {
        diskRadius = 0.5F;
        gravity = 0;
        boundsSize = new Vector2(10, 10);
        collisionDamping = 1.0F;
        mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
        stringWidth = 0.1F;
        numberOfTurns = 5;
        maxLength = 10F;
        stringLength = 5F;
        origine = Vector3.zero;
    }

    void DrawDisk(float diskRadius, Vector3 position)
    {
        DrawFilled(50, diskRadius, position);
    }

    void DrawFilled(int sides, float radius, Vector3 position)
    {
        polygonPoints = GetCircumferencePoints(sides, radius, position).ToArray();
        polygonTriangles = DrawFilledTriangles(polygonPoints);
        mesh.Clear();
        mesh.vertices = polygonPoints;
        mesh.triangles = polygonTriangles;
    }

    void DrawString(float width, Vector3 startPosition, int numberOfTurns, float length, float maxLength, Vector3 endPosition)
    {
        stringPoints = new List<Vector3>();
        stringPoints.Add(startPosition);
        for(int i=0; i<4*numberOfTurns; i++)
        {
            if (i % 4 == 0)
            {
                stringPoints.Add(new Vector3(maxLength * Mathf.Sin(Mathf.Acos(length / maxLength)) / (2 * numberOfTurns), (i + 1) * length / (4 * numberOfTurns), 0));

            } else
            {
                stringPoints.Add(new Vector3(-1*maxLength * Mathf.Sin(Mathf.Acos(length / maxLength)) / (2 * numberOfTurns), (i + 1) * length / (4 * numberOfTurns), 0));
            }
            i++;
            stringPoints.Add(new Vector3(0, (i + 1) * length / (4 * numberOfTurns), 0));
        }
        stringPoints.Add(endPosition);
        lineRenderer.positionCount = stringPoints.Count;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.SetPositions(stringPoints.ToArray());
    }

    void DrawLine(float width)
    {
        Vector3 a = new Vector3(0, 0, 0);
        Vector3 b = new Vector3(0, 1, 0);
        Vector3 c = new Vector3(1, 0, 0);
        List<Vector3> positions = new List<Vector3>();
        positions.Add(a);
        positions.Add(b);
        positions.Add(c);
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.SetPositions(positions.ToArray());
    }
    List<Vector3> GetCircumferencePoints(int sides, float radius, Vector3 position)
    {
        List<Vector3> points = new List<Vector3>();
        float circumferenceProgressPerStep = (float)1 / sides;
        float TAU = 2 * Mathf.PI;
        float radianProgressPerStep = circumferenceProgressPerStep * TAU;

        for (int i = 0; i < sides; i++)
        {
            float currentRadian = radianProgressPerStep * i;
            points.Add(new Vector3(Mathf.Cos(currentRadian) * radius, Mathf.Sin(currentRadian) * radius, 0) + position);
        }
        return points;
    }

    int[] DrawFilledTriangles(Vector3[] points)
    {
        int triangleAmount = points.Length - 2;
        List<int> newTriangles = new List<int>();
        for (int i = 0; i < triangleAmount; i++)
        {
            newTriangles.Add(0);
            newTriangles.Add(i + 2);
            newTriangles.Add(i + 1);
        }
        return newTriangles.ToArray();
    }

    public void ResolveCollision()
    {
        Vector2 halfBoundSize = boundsSize / 2 - Vector2.one * diskRadius;

        if(Mathf.Abs(position.x) > halfBoundSize.x)
        {
            position.x = halfBoundSize.x * Mathf.Sign(position.x);
            velocity.x *= -1 * collisionDamping;
        }

        if (Mathf.Abs(position.y) > halfBoundSize.y)
        {
            position.y = halfBoundSize.y * Mathf.Sign(position.y);
            velocity.y *= -1 * collisionDamping;
        }
    }

    void Update()
    {
        velocity += Vector2.down * gravity * Time.deltaTime;
        position += velocity * Time.deltaTime;
        ResolveCollision();
        DrawDisk(diskRadius, position);
        DrawString(stringWidth, origine, numberOfTurns, position.y, maxLength, position);
    }
}
