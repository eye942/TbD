using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathCreator))]
public class Follower : MonoBehaviour
{
    private PathCreator pathCreator;
    public float speed = 1;
    float distanceTravelled;
    VertexPath path;

    void Start()
    {
        Vector2[] points;
        points = new Vector2[7];
        points[0] = (Vector2)gameObject.transform; //starting-point
        //points[0] = gameObject.transform; //starting-point
        points[6] = new Vector2(9, 1); //ending-point
        points[1] = new Vector2(Random.Range(-17, -11), Random.Range(2, 7));
        points[2] = new Vector2(Random.Range(-11, -6), Random.Range(2, 7));
        points[3] = new Vector2(Random.Range(-6, -1), Random.Range(2, 7));
        points[4] = new Vector2(Random.Range(-1, 3), Random.Range(2, 7));
        points[5] = new Vector2(Random.Range(3, 8), Random.Range(2, 7));

        BezierPath bezierPath = new BezierPath(points, false, PathSpace.xy);
        pathCreator = GetComponent<PathCreator>();
        pathCreator.bezierPath = bezierPath;

        //path = GeneratePath(points, false);

    }


    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
    }

}
