using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller<UIInstaller>
{
    [SerializeField] BuildDialog prefab_BuildDialog;
    [SerializeField] BuildDialogItem prefab_BuildDialogItem;

    public override void InstallBindings()
    {
        Container.BindFactory<BuildDialog, BuildDialog.Factory>().FromComponentInNewPrefab(this.prefab_BuildDialog);
        Container.BindFactory<BuildDialogItem, BuildDialogItem.Factory>().FromComponentInNewPrefab(this.prefab_BuildDialogItem);
    }
}