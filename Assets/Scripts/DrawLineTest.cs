using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLineTest : MonoBehaviour
{
    public Button mItem;

    public int mWidth = 120;
    public int mHeight = 60;

    private List<List<Item>> gameObjects;
    private GridLayoutGroup layoutGroup;

    private int _x1 = -1, _y1 = -1, _x2 = -1, _y2 = -1;

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

    void OnClick(int x, int y)
    {
        gameObjects[x][y].SetColor(Color.red);

        if (_x1 == -1)
        {
            _x1 = x;
            _y1 = y;
            return;
        }
        _x2 = x;
        _y2 = y;
        DrawLine(_x1, _y1, _x2, _y2);
    }

    private void Reset()
    {
        for (int i = 0; i < mHeight; i++)
        {
            for (int j = 0; j < mWidth; j++)
            {
                gameObjects[i][j].SetColor(Color.white);
            }
        }
    }

    private void DrawLine(int x1, int y1, int x2, int y2)
    {

    }
}

public class Item
{
    int _x;
    int _y;
    Button _button;
    Image _image;
    Action<int, int> _action;

    public Item(Button button, int x, int y, Action<int, int> action)
    {
        _x = x;
        _y = y;
        _button = button;
        _action = action;
        _button.onClick.AddListener(OnClick);
        _image = button.GetComponent<Image>();
    }

    void OnClick()
    {
        _action(_x, _y);
    }

    public void SetColor(Color c)
    {
        _image.color = c;
    }
}