using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[ExecuteAlways]
public class CircleLayout : UIBehaviour
{
    protected CircleLayout()
    { }

    [SerializeField] protected float m_OffsetDegree = 0;

    /// <summary>
    /// 偏移角度.
    /// </summary>
    public float offsetDegree { get { return m_OffsetDegree; } set { SetProperty(ref m_OffsetDegree, value); } }

    [SerializeField] protected bool m_ChildForceExpand = true;

    /// <summary>
    /// 是否填充整个圆.
    /// </summary>
    public bool childForceExpand { get { return m_ChildForceExpand; } set { SetProperty(ref m_ChildForceExpand, value); } }

    [SerializeField] protected float m_SpacingDegree = 0;

    /// <summary>
    /// 角度差，只会在childForceExpand关闭时起作用.
    /// </summary>
    public float spacing { get { return m_SpacingDegree; } set { SetProperty(ref m_SpacingDegree, value); } }

    [SerializeField] protected float m_ChildLengthToCenter = 100;
    /// <summary>
    /// Length of child elements to circle center.
    /// </summary>
    public float childLengthToCenter { get { return m_ChildLengthToCenter; } set { SetProperty(ref m_ChildLengthToCenter, value); } }

    [System.NonSerialized] private RectTransform m_Rect;
    protected RectTransform rectTransform
    {
        get
        {
            if (m_Rect == null)
                m_Rect = GetComponent<RectTransform>();
            return m_Rect;
        }
    }

    [System.NonSerialized] private List<RectTransform> m_RectChildren = new List<RectTransform>();
    protected List<RectTransform> rectChildren { get { return m_RectChildren; } }

    // 计算整个布局
    public void CalculateLayoutGroup()
    {
        RefreshChildren();
        CalculateElementsPosition();
    }

    // 刷新子物体
    void RefreshChildren()
    {
        m_RectChildren.Clear();
        for (int i = 0; i < rectTransform.childCount; i++)
        {
            var rect = rectTransform.GetChild(i) as RectTransform;
            if (rect == null || !rect.gameObject.activeInHierarchy)
                continue;
            m_RectChildren.Add(rect);
        }
    }

    const float DEGREE = 360.0f;
    // 计算各个物体的位置
    public void CalculateElementsPosition()
    {
        m_SpacingDegree = m_ChildForceExpand ? DEGREE / m_RectChildren.Count : m_SpacingDegree;
        for (int i = 0; i < m_RectChildren.Count; i++)
        {
            float radians = (i * m_SpacingDegree + offsetDegree) * Mathf.Deg2Rad;
            var newPos = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * m_ChildLengthToCenter;
            m_RectChildren[i].anchoredPosition = newPos;
        }
    }

    protected void SetProperty<T>(ref T currentValue, T newValue)
    {
        if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
            return;
        currentValue = newValue;
        CalculateLayoutGroup();
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        CalculateLayoutGroup();
    }
#endif

    protected override void OnEnable()
    {
        base.OnEnable();
        CalculateLayoutGroup();
    }

    protected override void OnDidApplyAnimationProperties()
    {
        CalculateLayoutGroup();
    }

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        CalculateLayoutGroup();
    }

    protected virtual void OnTransformChildrenChanged()
    {
        CalculateLayoutGroup();
    }
}
