using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DRAW_TYPE
{
    DDA,
    MiddleBresenham,
    Bresenham
}

public class DrawLineTest : MonoBehaviour
{
    public Button mItem;

    public int mWidth = 120;
    public int mHeight = 60;

    private List<List<Item>> gameObjects;
    private GridLayoutGroup layoutGroup;

    private int _x1 = -1, _y1 = -1, _x2 = -1, _y2 = -1;

    public DRAW_TYPE mDrawType;

    // Start is called before the first frame update
    void Start()
    {
        layoutGroup = GetComponent<GridLayoutGroup>();
        layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layoutGroup.constraintCount = mWidth;

        gameObjects = new List<List<Item>>(mHeight);
        for (int i = 0; i < mHeight; i++)
        {
            gameObjects.Add(new List<Item>(mWidth));
            for (int j = 0; j < mWidth; j++)
            {
                Button button = Instantiate<Button>(mItem, transform);
                gameObjects[i].Add(new Item(button, i, j, OnClick));
            }
        }

        mItem.gameObject.SetActive(false);
    }

    void OnClick(int row, int col)
    {
        if (_x1 == -1)
        {
            _x1 = col;
            _y1 = row;
        }
        else if (_x2 == -1)
        {
            _x2 = col;
            _y2 = row;
            switch (mDrawType)
            {
                case DRAW_TYPE.DDA:
                    DDADrawLine(_x1, _y1, _x2, _y2);
                    break;
                case DRAW_TYPE.MiddleBresenham:
                    break;
                case DRAW_TYPE.Bresenham:
                    break;
                default:
                    break;
            }
        }
        else
        {
            _x1 = col;
            _y1 = row;
            Clear();
        }
        Debug.Log("OnClick:(" + col + ", " + row + ")");
        gameObjects[row][col].SetColor(Color.green);
    }

    private void Clear()
    {
        for (int i = 0; i < mHeight; i++)
        {
            for (int j = 0; j < mWidth; j++)
            {
                gameObjects[i][j].SetColor(Color.white);
            }
        }
        _x2 = -1;
        _y2 = -1;
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
    private void DDADrawLine(int x1, int y1, int x2, int y2)
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
            gameObjects[(int)(y + 0.5f)][(int)(x + 0.5f)].SetColor(Color.red);
        }
    }
}

public class Item
{
    int _row;
    int _col;
    Button _button;
    Image _image;
    Action<int, int> _action;

    public Item(Button button, int row, int col, Action<int, int> action)
    {
        _row = row;
        _col = col;
        _button = button;
        _action = action;
        _button.onClick.AddListener(OnClick);
        _image = button.GetComponent<Image>();
    }

    void OnClick()
    {
        _action(_row, _col);
    }

    public void SetColor(Color c)
    {
        _image.color = c;
    }
}