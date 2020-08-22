using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    public float mInterval = 0.5f;
    private float mTime = 0.0f;
    private int mFrame = 0;
    private float mAccum = 0.0f;

    private string mFPS = "0";

    // Start is called before the first frame update
    void Start()
    {
        mTime = mInterval;
    }

    // Update is called once per frame
    void Update()
    {
        mTime -= Time.deltaTime;
        mAccum += Time.timeScale / Time.deltaTime;
        ++mFrame;
        if (mTime < 0.0f)
        {
            mFPS = string.Format("{0:F2} FPS", mAccum / mFrame);
            mTime = mInterval;
            mAccum = 0.0f;
            mFrame = 0;
        }
    }

    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(GUIStyle.none);
        guiStyle.normal.textColor = Color.red;
        GUI.Label(new Rect(new Vector2(10, 10), new Vector2(100, 50)), mFPS, guiStyle);
    }
}
