using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallOrder : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(gameObject.name + "Awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name + "Start");
    }

    private void OnEnable()
    {
        Debug.Log(gameObject.name + "OnEnable");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.name + "Update");
    }

    private void FixedUpdate()
    {
        Debug.Log(gameObject.name + "FixedUpdate");
    }

    private void LateUpdate()
    {
        Debug.Log(gameObject.name + "LateUpdate");
    }

    private void OnDisable()
    {
        Debug.Log(gameObject.name + "OnDisable");
    }

    private void OnDestroy()
    {
        Debug.Log(gameObject.name + "OnDestroy");
    }

    public void Test()
    {
        //List<int> list = new List<int>() { 1, 5, 6, 7, 8, 9 };
        //list.FindAll(a => a > 5);
        //list.FindAll(int a => a > 5);
        //list.FindAll((int a) => a > 5);
        //list.FindAll((int a) => { return a > 5; });
    }
}
