using System;
using System.Collections;
using UnityEngine;

public class DrawLine
{
    Action<int, int> _drawFun;

    public DrawLine(Action<int, int> drawFun)
    {
        _drawFun = drawFun;
    }

    /// <summary>
    /// DDA
    /// 优点：
    ///     增量算法（每一步都是上一步的x,y值增加一个量）
    ///     直观、易实现
    /// 缺点：
    ///     需要进行浮点数运算；
    ///     产生一个像素要做两次加法和两次取整运算；
    ///     运行效率低；
    ///     取整运算不利于硬件实现。
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    public void DDADrawLine(int x1, int y1, int x2, int y2)
    {
        int step;
        int dx = x2 - x1;
        int dy = y2 - y1;
        if (Math.Abs(dy) > Math.Abs(dx))
        {
            step = Math.Abs(y2 - y1);
        }
        else
        {
            step = Math.Abs(x2 - x1);
        }

        float incrementX = (float)dx / step;
        float incrementY = (float)dy / step;

        float x = x1;
        float y = y1;

        for (int i = 0; i < step; i++)
        {
            x += incrementX;
            y += incrementY;
            _drawFun((int)(x + 0.5f), (int)(y + 0.5f));
        }
    }

    /// <summary>
    /// 中点Bresenham算法
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    public void MiddleBresenhamDrawLine(int x1, int y1, int x2, int y2)
    {
        int dx = x2 - x1;
        int dy = y2 - y1;

        float k = (float)dy / dx;

        int sx = dx > 0 ? 1 : -1;
        int sy = dy > 0 ? 1 : -1;

        bool flag = Math.Abs(dy) > Math.Abs(dx);

        float d = 0;

        float absK = Mathf.Abs(k);
        float abs1DivK = Mathf.Abs(1 / k);

        int x = x1;
        int y = y1;
        while (x != x2 || y != y2)
        {
            _drawFun(x, y);

            if (flag)
            {
                y += sy;

                d += abs1DivK;
                if (d > 0.5f)
                {
                    x += sx;
                    d -= 1;
                }
            }
            else
            {
                x += sx;

                d += absK;
                if (d > 0.5f)
                {
                    y += sy;
                    d -= 1;
                }
            }
        }
    }

    /// <summary>
    /// 优化的Bresenham算法
    /// 1. e = d - 0.5
    /// 2. E = 2e▲x
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    public void BresenhamDrawLine(int x1, int y1, int x2, int y2)
    {
        int dx = x2 - x1;
        int dy = y2 - y1;

        int sx = dx > 0 ? 1 : -1;
        int sy = dy > 0 ? 1 : -1;

        bool flag = Math.Abs(dy) > Math.Abs(dx);

        int E = flag ? -sy * dy : -sx * dx;

        int absDx = sx * dx;
        int absDy = sy * dy;

        int x = x1;
        int y = y1;
        while (x != x2 || y != y2)
        {
            _drawFun(x, y);

            if (flag)
            {
                y += sy;

                E += absDx << 1;
                if (E > 0)
                {
                    x += sx;
                    E -= absDy << 1;
                }
            }
            else
            {
                x += sx;

                E += absDy << 1;
                if (E > 0)
                {
                    y += sy;
                    E -= absDx << 1;
                }
            }
        }
    }
}