using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnTest : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Text text;
    RotateCtrl rotateTest;
    public GameObject centerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        btn1.onClick.AddListener(BtnClick1);
        btn2.onClick.AddListener(BtnClick2);
        rotateTest = centerGameObject.GetComponent<RotateCtrl>();
    }

    void BtnClick1()
    {
        rotateTest.PlayCircle(float.Parse(text.text), false);
    }

    void BtnClick2()
    {
        rotateTest.ResetCircle();
    }

    // Update is called once per frame
    void Update()
    {
        rotateTest.UpdateCircle();
    }
}
