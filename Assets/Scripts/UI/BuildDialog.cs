using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class BuildDialog : MonoBehaviour, IOpenCloseable
{

    [SerializeField] private RectTransform frame;
    [SerializeField] RectTransform itemsFrame;

    public const float ICON_MARGIN = 8f;

    private BuildableSlot slot;



    public void Init(BuildableSlot slot)
    {
        this.slot = slot;
        this.frame = this.gameObject.GetComponent<RectTransform>();
        BuildDialogItem item = null;
        for (int i = 0; i < slot.PossibleBuildables.Length; i++)
        {
            item = Root.UIFactory.CreateBuildDialogItem(slot.PossibleBuildables[i], this.itemsFrame);
            item.GetComponent<RectTransform>().localPosition =
                new Vector3(ICON_MARGIN, 0f, 0f) +
                new Vector3((BuildDialogItem.FRAME_WIDTH + ICON_MARGIN) * i, 0f, 0f);
            item.Clicked += Item_Clicked;
        }
    }

    private void Item_Clicked(Buildable buildable)
    {
        Root.BuildUI.Close();
        Root.BuildSystem.Build(buildable, this.slot);
    }



    public void Open(bool instant = false)
    {
        throw new System.NotImplementedException();
    }

    public void Close(bool instant = false, System.Action callback = null)
    {
        //TODO: Actually implement closing behaviour!
        if (callback != null)
            callback.Invoke();
    }

    public bool IsOpen()
    {
        throw new System.NotImplementedException();
    }
}
