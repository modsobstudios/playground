using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Undulate : MonoBehaviour
{

    [SerializeField]
    private bool xAxis = false;
    [SerializeField]
    private bool yAxis = false;
    [SerializeField]
    private bool zAxis = false;

    private Vector3 PointA;
    private Vector3 PointB;

    [SerializeField]
    private float distance = 0.0f;
    private float traveled = 0.0f;
    [SerializeField]
    private float speed = 2.0f;

    private Vector3 start;

    // Use this for initialization
    void Start()
    {
        start = gameObject.transform.position;
        PointA = start;
        PointB = new Vector3(start.x + distance, start.y + distance, start.z + distance);
    }

    // FixedUpdate is not called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(PointA,
            new Vector3(
            start.x + distance * Convert.ToInt32(xAxis),
            start.y + distance * Convert.ToInt32(yAxis),
            start.z + distance * Convert.ToInt32(zAxis)),
            Mathf.PingPong(Time.time * speed, 1));
    }
}
