using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float m_CamSpeed = 0.50f;
    private GameObject m_Camera;

    private void Awake()
    {
        m_Camera = transform.gameObject;
    }

    void Update()
    {
        m_CamSpeed = 0.50f;
        //if (Input.mousePosition.y <= 2) // If the mouse is at the bottom of the visible screen or lower then continue
        //{
        //    m_CamSpeed = 0.0f;
        //}
        //if (Input.mousePosition.y >= Screen.height - 2) // If the mouse is at the top of the visible screen or higher then continue
        //{
        //    m_CamSpeed = 0.0f;
        //}
        //if (Input.mousePosition.x <= 2) // If the mouse is at the left of the visible screen or lower then continue
        //{
        //    m_CamSpeed = 0.0f;
        //}
        //if (Input.mousePosition.x >= Screen.width - 2) // If the mouse is at the right of the visible screen or higher then continue
        //{
        //    m_CamSpeed = 0.0f;
        //}
        float xDelta = Input.GetAxis("Mouse X");
        float yDelta = Input.GetAxis("Mouse Y");

        m_Camera.transform.localRotation *= Quaternion.Euler(new Vector3(-yDelta, 0, 0) * m_CamSpeed);
        m_Camera.transform.localRotation *= Quaternion.Euler(new Vector3(0, xDelta, 0) * m_CamSpeed);
    }
}
