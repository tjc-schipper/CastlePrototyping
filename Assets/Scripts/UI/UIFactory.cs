using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ING/Factories/UIFactory")]
public class UIFactory : ScriptableObject
{

    [SerializeField] BuildDialog prefab_BuildDialog;
    [SerializeField] BuildDialogItem prefab_BuildDialogItem;

    public BuildDialog CreateBuildDialog(BuildableSlot slot, RectTransform parent)
    {
        BuildDialog newDialog = Instantiate(prefab_BuildDialog, parent);
        newDialog.Init(slot);
        return newDialog;
    }

    public BuildDialogItem CreateBuildDialogItem(Buildable buildable, RectTransform parent)
    {
        BuildDialogItem item = Instantiate(this.prefab_BuildDialogItem, parent);
        item.Init(buildable);
        return item;
    }
}
