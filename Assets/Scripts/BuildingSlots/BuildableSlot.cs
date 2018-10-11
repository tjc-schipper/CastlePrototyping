using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clickable))]
public class BuildableSlot : MonoBehaviour
{

    public Buildable[] PossibleBuildables;

    private Clickable clickable;

    [Zenject.Inject]
    private BuildUI buildUI;    // Invert this dependency! buildUI should respond to slots being clicked.

    public enum SlotStates
    {
        DISABLED = 0,
        EMPTY,
        BUILDING,
        OCCUPIED
    }

    public SlotStates SlotState { get; private set; }
    private GameObject currentBuildable;
    private bool selected = false;

    private void Awake()
    {
        this.clickable = GetComponent<Clickable>();
        this.clickable.Click += Clickable_Click;
        this.clickable.MouseOver += Clickable_MouseOver;
        this.clickable.MouseOut += Clickable_MouseOut;

        SetHighlighted(false);

        this.SlotState = SlotStates.EMPTY;
    }


    public void AssignContent(GameObject buildable)
    {
        this.SlotState = SlotStates.OCCUPIED;
        this.currentBuildable = buildable;
    }


    public void Deselect()
    {
        this.selected = false;
        SetHighlighted(false);
    }

    private void SetHighlighted(bool hl)
    {
        Color c = hl ?
            Color.green :
            Color.white;
        GetComponent<Renderer>().material.color = c;
    }


    private void Clickable_Click(Clickable sender)
    {
        if (this.SlotState == SlotStates.EMPTY)
            Root.BuildUI.ShowBuildDialog(this);
    }

    private void Clickable_MouseOver(Clickable sender)
    {
        SetHighlighted(true);
    }

    private void Clickable_MouseOut(Clickable sender)
    {
        if (!selected)
        {
            SetHighlighted(false);
        }
    }




}
