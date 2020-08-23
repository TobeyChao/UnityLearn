using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragOperation : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Action<GameObject, PointerEventData> onBeginDrag;
    public Action<GameObject, PointerEventData> onEndDrag;
    public Action<GameObject, PointerEventData> onDrag;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag?.Invoke(gameObject, eventData);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        onEndDrag?.Invoke(gameObject, eventData);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(gameObject, eventData);
        //transform.position = eventData.position;
    }
}