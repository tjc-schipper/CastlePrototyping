using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clickable))]
public class TestClickable : MonoBehaviour
{

    Material mat;

    private void Start()
    {
        Clickable c = this.gameObject.GetComponent<Clickable>();
        c.MouseOver += C_MouseOver;
        c.MouseOut += C_MouseOut;

        c.MouseDown += C_MouseDown;
        c.MouseUp += C_MouseUp;
        c.Click += C_Click;
    }


    private void C_Click(Clickable sender)
    {
        Debug.Log("CLICK");
    }

    private void C_MouseUp(Clickable sender)
    {
        Debug.Log("UP");
    }

    private void C_MouseDown(Clickable sender)
    {
        Debug.Log("DOWN");
    }

    private void C_MouseOut(Clickable sender)
    {
        Debug.Log("mouse exit");
    }

    private void C_MouseOver(Clickable sender)
    {
        Debug.Log("mouse enter");
    }
}
