using UnityEngine;
using Zenject;

public class PrototypeInstaller : MonoInstaller<PrototypeInstaller>
{
    [SerializeField] BuildableSet buildableSet;
    [SerializeField] BuildUI buildUI;
    [SerializeField] UIFactory uiFactory;

    public override void InstallBindings()
    {
        Container.Bind<BuildableSet>().FromInstance(this.buildableSet).AsSingle();
        Container.Bind<BuildUI>().FromInstance(this.buildUI).AsSingle();
        Container.Bind<UIFactory>().FromInstance(this.uiFactory).AsSingle();
    }
}