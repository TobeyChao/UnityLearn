using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCtrl : MonoBehaviour
{
    public float speedScaleOrigin = 20;
    public float speedScale = 1;
    public float degree = 0;
    public float targetDegree = 0;
    public bool playing = false;
    public float state = 1;

    public void PlayCircle(float targetDegreeIn, bool limited = true)
    {
        targetDegree = limited ? (targetDegreeIn % 360) : targetDegreeIn;
        if (Mathf.Abs(targetDegree - degree) <= 1e-6f)
        {
            return;
        }
        state = targetDegree - degree;
        speedScale = speedScaleOrigin * (state > 0 ? 1 : -1);
        playing = true;
    }

    public void ResetCircle()
    {
        targetDegree = 0;
        state = 1;
        speedScale = 20;
        playing = false;
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    public void UpdateCircle()
    {
        // 判断是不是该停
        if (playing && state * (targetDegree - degree) < 0)
        {
            degree = targetDegree;
            playing = false;
        }
        // 判断增减
        if (playing)
        {
            degree = degree + Time.fixedDeltaTime * speedScale;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree));
        }
    }
}
