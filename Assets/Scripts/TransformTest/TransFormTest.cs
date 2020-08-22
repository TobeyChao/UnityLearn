using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransFormTest : MonoBehaviour
{
    private float agent;
    private float translateX;
    private float degreePerFrame = 50;
    void Start()
    {
        agent = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        agent += Time.deltaTime * 0.05f;
        translateX += Time.deltaTime * 0.2f;
        // 1. localEulerAngles就是根据父物体坐标系
        //transform.localEulerAngles = new Vector3(0f, agent, 0f);
        //transform.localRotation = Quaternion.Euler(new Vector3(0f, agent, 0f));
        //transform.localPosition = new Vector3(translateX, 0, 0);
        // 2. eulerAngles等不带local的就是根据场景的世界坐标系
        //transform.eulerAngles = new Vector3(0f, agent, 0f);
        // 3. 围绕过点point的直线axis旋转angle角度
        transform.RotateAround(new Vector3(0, 1, 0), new Vector3(1, 1, 1), Time.deltaTime * degreePerFrame);
        Debug.DrawLine(new Vector3(-2, -1, -2), new Vector3(3, 4, 3), Color.red);
    }
}
