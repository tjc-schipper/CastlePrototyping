using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : MonoBehaviour, IOpenCloseable
{

    [SerializeField] RectTransform root_BuildDialog;

    [Zenject.Inject]
    UIFactory uiFactory;

    private BuildDialog buildDialog;
    private bool isOpen = false;


    public void ShowBuildDialog(BuildableSlot slot)
    {
        if (this.buildDialog != null)
        {
            BuildDialog oldDialog = this.buildDialog;
            this.buildDialog.Close(false, () =>
            {
                Destroy(oldDialog.gameObject);
            });
        }

        this.buildDialog = this.uiFactory.CreateBuildDialog(slot, this.root_BuildDialog);
        this.isOpen = true;
    }


    public void Close(bool instant = false, System.Action callback = null)
    {
        if (this.buildDialog != null)
        {
            BuildDialog oldDialog = this.buildDialog;
            this.buildDialog.Close(instant, () =>
            {
                Destroy(oldDialog.gameObject);
            });
            this.buildDialog = null;
        }

        this.isOpen = false;
    }

    public bool IsOpen()
    {
        return this.isOpen;
    }

    public void Open(bool instant = false)
    {
        throw new System.NotImplementedException();
    }
}
