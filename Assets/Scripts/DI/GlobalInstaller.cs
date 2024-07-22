using Assets.Scripts.Misc.Utils;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        bindMisc();
    }

    private void bindMisc()
    {
        Container.BindInterfacesAndSelfTo<Routine>().
            FromNewComponentOnNewGameObject().AsSingle();
    }
}