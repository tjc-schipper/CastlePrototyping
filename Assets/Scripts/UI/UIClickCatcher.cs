using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickCatcher : MonoBehaviour
{
    public delegate void VoidEvent();
    public event VoidEvent OnClickNothing;

    private void Start()
    {
        OnClickNothing += () =>
        {
            Debug.Log("Caught");
        };
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
            if (!UIHelpers.IsMouseOverUI())
                if (OnClickNothing != null)
                    OnClickNothing.Invoke();
    }
}
