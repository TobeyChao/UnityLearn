using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 在Unity中，在检视面板中可以看到的，就是被成功序列化了的参数
 */
public class SerializedTest : MonoBehaviour
{
    // public字段可以被序列化
    public int number;

    // [NonSerialized]使public字段不可以被序列化
    [NonSerialized]
    public int nonSerializedNumber;

    // [NonSerialized]使public字段不可以被序列化
    [HideInInspector]
    public int hideInInspectorNumber;

    // 加了SerializeField可以使私有字段序列化
    [SerializeField]
    private string myName;

    // List能不能序列化和类型有关，见于MyClass和MyClassS
    public List<ItemInfo> itemInfoList;

    // 加了[Serializable]可以使类序列化
    [Serializable]
    public class ItemInfo
    {
        // 基本类型被序列化
        public string typeName;
        // 游戏物体被序列化
        public GameObject gameObject;
        // 加了SerializeField可以使类中的私有字段序列化，但是非[Serializable]的类依旧不能
        [SerializeField]
        private int id;
        // private 类型的不能被序列化
        private int hideId;
        // 非[Serializable]不能被序列化
        public MyClassNS myClassNS;
        // [Serializable]被序列化
        public List<MyClassS> myClassS;
    }

    public class MyClassNS
    {
        public int testField;
    }

    [Serializable]
    public class MyClassS
    {
        public int testField;
    }
}
