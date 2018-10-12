using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public delegate void ClickEvent(Clickable sender);

    public event ClickEvent Click;
    public event ClickEvent MouseOver;
    public event ClickEvent MouseStay;
    public event ClickEvent MouseOut;

    public event ClickEvent MouseDown;
    public event ClickEvent MouseUp;
    public event ClickEvent MouseHold;

    private enum ClickModes
    {
        NONE = 0,
        CLICK,
        UP_ONLY
    }

    private const float CLICK_THRESHOLD = 0.1f;
    private const float UPDATE_RATE = 60f;

    private bool mouseDown;
    private bool over;
    private ClickModes clickMode;
    private Coroutine cr_mouseDown;
    private Coroutine cr_updateEvents;

    private void Start()
    {
        this.cr_updateEvents = StartCoroutine(CR_UpdateEvents());
    }

    private bool IsCoveredByUI()
    {
        // Prevent clicking if covered by UI element
        UnityEngine.EventSystems.EventSystem es = UnityEngine.EventSystems.EventSystem.current;
        return (es != null && es.IsPointerOverGameObject());
    }

    private void OnMouseEnter()
    {
        if (IsCoveredByUI()) return;

        this.over = true;
        if (this.MouseOver != null)
            this.MouseOver.Invoke(this);
    }

    private void OnMouseExit()
    {
        if (IsCoveredByUI()) return;

        this.over = false;
        if (this.MouseOut != null)
            this.MouseOut.Invoke(this);
    }

    private void OnMouseDown()
    {
        if (IsCoveredByUI()) return;

        this.mouseDown = true;

        if (this.cr_mouseDown != null)
            StopCoroutine(this.cr_mouseDown);

        this.clickMode = ClickModes.CLICK;
        this.cr_mouseDown = StartCoroutine(CR_ClickTimer());
    }

    private void OnMouseUp()
    {
        if (IsCoveredByUI()) return;

        this.mouseDown = false;

        if (this.clickMode != ClickModes.NONE)
        {
            if (this.MouseUp != null)
                this.MouseUp.Invoke(this);

            if (this.clickMode == ClickModes.CLICK && this.over)    // Cannot click if you're not still pointing at the object
                if (this.Click != null)
                    this.Click.Invoke(this);

            this.clickMode = ClickModes.NONE;
        }

        if (this.cr_mouseDown != null)
        {
            StopCoroutine(this.cr_mouseDown);
            this.cr_mouseDown = null;
        }
    }


    private IEnumerator CR_ClickTimer()
    {
        yield return new WaitForSecondsRealtime(CLICK_THRESHOLD);
        this.clickMode = ClickModes.UP_ONLY;
    }

    private IEnumerator CR_UpdateEvents()
    {
        while (true)
        {
            if (this.over)
                if (this.MouseStay != null)
                    this.MouseStay.Invoke(this);

            if (this.mouseDown)
                if (this.MouseHold != null)
                    this.MouseHold.Invoke(this);

            yield return new WaitForSecondsRealtime(1f / UPDATE_RATE);
        }
    }

}
