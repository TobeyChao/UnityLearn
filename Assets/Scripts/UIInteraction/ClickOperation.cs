using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ClickOperation : MonoBehaviour, IPointerClickHandler
{
    public Action<GameObject> onClick;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(eventData.pointerClick);
    }
}