﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnTest : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Text text;
    RotateCtrl rotateTest;
    public GameObject centerGameObject;
    public List<ClickOperation> clickOperations;
    public List<DragOperation> dragOperations;

    // Start is called before the first frame update
    void Start()
    {
        btn1.onClick.AddListener(BtnClick1);
        btn2.onClick.AddListener(BtnClick2);
        rotateTest = centerGameObject.GetComponent<RotateCtrl>();
        for (int i = 0; i < clickOperations.Count; i++)
        {
            clickOperations[i].onClick = OnImageClicked;
        }
        for (int i = 0; i < dragOperations.Count; i++)
        {
            dragOperations[i].onDrag = OnImageDrag;
            dragOperations[i].onBeginDrag = OnImageBeginDrag;
        }
    }

    void BtnClick1()
    {
        rotateTest.PlayCircle(float.Parse(text.text), false);
    }

    void BtnClick2()
    {
        rotateTest.ResetCircle();
    }

    Vector2 beginPoint = new Vector2(0, 0);
    float startDegree = 0;

    void OnImageBeginDrag(GameObject gameObject, PointerEventData pointerEventData)
    {
        beginPoint = pointerEventData.position - new Vector2(centerGameObject.transform.position.x, centerGameObject.transform.position.y);
        startDegree = rotateTest.degree;
        Debug.Log(beginPoint.ToString());
    }

    void OnImageDrag(GameObject gameObject, PointerEventData pointerEventData)
    {
        var vec1 = beginPoint;
        var vec2 = pointerEventData.position - new Vector2(centerGameObject.transform.position.x, centerGameObject.transform.position.y);
        var degreeDelta = Mathf.Rad2Deg * Mathf.Acos(Vector2.Dot(vec1, vec2) / (vec1.magnitude * vec2.magnitude));
        rotateTest.PlayCircle(startDegree + degreeDelta, false);
    }

    void OnImageClicked(GameObject gameObject)
    {
        //for (int i = 0; i < clickOperations.Count; i++)
        //{
        //    if (clickOperations[i].gameObject == gameObject)
        //    {
        //        rotateTest.PlayCircle(-i * 60, false);
        //        break;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        rotateTest.UpdateCircle();
    }
}
