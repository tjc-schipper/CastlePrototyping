using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIFactory : MonoBehaviour
{
    [Inject]
    private BuildDialog.Factory fact_BuildDialog;

    [Inject]
    private BuildDialogItem.Factory fact_BuildDialogItem;

    public BuildDialog CreateBuildDialog(BuildableSlot slot, RectTransform parent)
    {
        BuildDialog newDialog = this.fact_BuildDialog.Create();
        newDialog.transform.SetParent(parent);
        newDialog.transform.localPosition = Vector3.zero;
        newDialog.Init(slot);
        return newDialog;
    }

    public BuildDialogItem CreateBuildDialogItem(Buildable buildable, RectTransform parent)
    {
        BuildDialogItem item = this.fact_BuildDialogItem.Create();
        item.transform.SetParent(parent);
        item.transform.localPosition = Vector3.zero;
        item.Init(buildable);
        return item;
    }

}
