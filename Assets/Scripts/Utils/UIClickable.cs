using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClickable : MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler
{
    public delegate void UIClickableEvent(UIClickable sender);
    public event UIClickableEvent Click;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click intercepted! " + eventData.pointerPress.name);
        if (Click != null)
            Click.Invoke(this);
    }
}
