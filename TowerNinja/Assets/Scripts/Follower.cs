using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathCreation;
using PathCreation.Utility;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Follower : MonoBehaviour
{
    private PathCreator pathCreator;
    private VertexPath path;
    public float speed = 1;
    float distanceTravelled;

    void Start()
    {
        IEnumerable<Vector2> points = new List<Vector2>
        {
            new Vector2( -17,0),
            new Vector2(Random.Range(-17, -11.5f), Random.Range(2, 7)),
            new Vector2(Random.Range(-11.5f, -6.4f), Random.Range(2, 7)),
            new Vector2(Random.Range(-6.4f, -1.3f), Random.Range(2, 7)),
            new Vector2(Random.Range(-1.3f, 3.8f), Random.Range(2, 7)),
            new Vector2(Random.Range(3.8f, 8.9f), Random.Range(2, 7)),
            new Vector2(9.0f, -3.0f),
    
        };
        //points = points.Select(p => p + new Vector2(17, 0));

        pathCreator = GetComponent<PathCreator>();
        print(pathCreator.transform.position);


        BezierPath bezierPath = new BezierPath(points, false);
        path = new VertexPath(bezierPath, GameObject.Find("ORIGIN").transform);
        //path = GeneratePath(points, false);
        transform.position = pathCreator.path.GetPoint(0);
        print(path.NumPoints);
    }

    private void OnDrawGizmos()
    {
        var bounds = pathCreator.path.bounds;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(bounds.center,bounds.size);
    }


    void Update()
    {
        //pathCreator.path.UpdateTransform(pathCreator.transform);
        print($"{transform.position}:{pathCreator.transform.position}");
        distanceTravelled += speed * Time.deltaTime;
        transform.position = path.GetPointAtDistance(distanceTravelled);
    }
}
