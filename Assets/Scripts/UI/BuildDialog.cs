using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class BuildDialog : MonoBehaviour, IOpenCloseable
{

    [SerializeField]
    RectTransform itemsFrame;

    public const float ICON_MARGIN = 8f;

    private UIFactory uiFactory;

    private BuildUI buildUI;
    private BuildSystem buildSystem;
    private PlayerResources playerResources;
    private BuildableSlot slot;


    [Zenject.Inject]
    public void Construct(UIFactory uiFactory, BuildUI buildUI, BuildSystem buildSystem, PlayerResources playerResources)
    {
        this.uiFactory = uiFactory;
        this.buildUI = buildUI;
        this.buildSystem = buildSystem;
        this.playerResources = playerResources;
    }

    public void Init(BuildableSlot slot)
    {
        this.slot = slot;
        BuildDialogItem item = null;
        for (int i = 0; i < slot.PossibleBuildables.Length; i++)
        {
            item = this.uiFactory.CreateBuildDialogItem(slot.PossibleBuildables[i], this.itemsFrame);

            item.Clicked += Item_Clicked;
            item.transform.localPosition =
                new Vector3(ICON_MARGIN, 0f, 0f) +
                new Vector3((BuildDialogItem.FRAME_WIDTH + ICON_MARGIN) * i, 0f, 0f);
        }
    }

    private void Item_Clicked(Buildable buildable)
    {
        if (this.playerResources.Pay(buildable.Cost))
        //if (Root.PlayerResources.Pay(buildable.Cost))
        {
            //Root.BuildUI.Close();
            //Root.BuildSystem.Build(buildable, this.slot);
            this.buildUI.Close();
            this.buildSystem.Build(buildable, this.slot);

        }
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



    public class Factory : Zenject.PlaceholderFactory<BuildDialog> { }
}
