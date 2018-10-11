using UnityEngine;
using Zenject;

public class PrototypeInstaller : MonoInstaller<PrototypeInstaller>
{
	[Header("Core components")]

	[SerializeField]
	BuildableSet buildableSet;

	[SerializeField]
	BuildUI buildUI;

	[SerializeField]
	BuildSystem buildSystem;

	[SerializeField]
	UIFactory uiFactory;

	[SerializeField]
	PlayerResources playerResources;

	[Header("UI Prefabs")]
	[SerializeField]
	BuildDialog prefab_BuildDialog;

	[SerializeField]
	BuildDialogItem prefab_BuildDialogItem;

	public override void InstallBindings()
	{
		Container.Bind<BuildableSet>().FromInstance(this.buildableSet).AsSingle();
		Container.Bind<BuildUI>().FromInstance(this.buildUI).AsSingle();
		Container.Bind<UIFactory>().FromInstance(this.uiFactory).AsSingle();
		Container.Bind<BuildSystem>().FromInstance(this.buildSystem).AsSingle();
		Container.Bind<PlayerResources>().FromInstance(this.playerResources).AsSingle();

		Container.BindFactory<BuildDialog, BuildDialog.Factory>().FromComponentInNewPrefab(this.prefab_BuildDialog);
		Container.BindFactory<BuildDialogItem, BuildDialogItem.Factory>().FromComponentInNewPrefab(this.prefab_BuildDialogItem);
	}
}