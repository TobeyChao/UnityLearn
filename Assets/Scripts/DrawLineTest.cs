using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    private DrawLine drawLine;

    public int mDrawCount = 100;

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

        drawLine = new DrawLine((x, y) =>
        {
            gameObjects[y][x].SetColor(Color.red);
        });
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

            Stopwatch sw = new Stopwatch();
            sw.Start();
            switch (mDrawType)
            {
                case DRAW_TYPE.DDA:
                    for (int i = 0; i < mDrawCount; i++)
                        drawLine.DDADrawLine(_x1, _y1, _x2, _y2);
                    break;
                case DRAW_TYPE.MiddleBresenham:
                    for (int i = 0; i < mDrawCount; i++)
                        drawLine.MiddleBresenhamDrawLine(_x1, _y1, _x2, _y2);
                    break;
                case DRAW_TYPE.Bresenham:
                    for (int i = 0; i < mDrawCount; i++)
                        drawLine.BresenhamDrawLine(_x1, _y1, _x2, _y2);
                    break;
                default:
                    break;
            }
            sw.Stop();
            long times = sw.ElapsedMilliseconds;
            UnityEngine.Debug.Log(mDrawType + " Cost:" + times + "ms");
        }
        else
        {
            _x1 = col;
            _y1 = row;
            Clear();
        }
        UnityEngine.Debug.Log("OnClick:(" + col + ", " + row + ")");
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