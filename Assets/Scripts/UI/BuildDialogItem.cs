using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildDialogItem : MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler
{

    public event System.Action<Buildable> Clicked;

    [SerializeField] UnityEngine.UI.Image icon;
    [SerializeField] UnityEngine.UI.Text nameLabel;
    [SerializeField] UnityEngine.UI.Text costLabel;

    public const float FRAME_WIDTH = 128f;
    public const float FRAME_HEIGHT = 200f;

    private Buildable buildable;

    public void Init(Buildable buildable)
    {
        this.buildable = buildable;
        this.icon.sprite = buildable.BuildMenuIcon;
        this.nameLabel.text = buildable.DisplayName;
        this.costLabel.text = buildable.Cost.materials.ToString();  //TODO: Swap with a resource visualizer! (stacked vertical)
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Clicked != null)
            Clicked.Invoke(this.buildable);
    }
}
