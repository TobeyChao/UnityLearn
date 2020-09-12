using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float m_CamSpeed = 500f;
    public GameObject m_Camera;

    void Update()
    {
        float xDelta = Input.GetAxis("Mouse X");
        float yDelta = Input.GetAxis("Mouse Y");
        m_Camera.transform.localEulerAngles += new Vector3(-yDelta, xDelta, 0) * m_CamSpeed * Time.deltaTime;
    }
}