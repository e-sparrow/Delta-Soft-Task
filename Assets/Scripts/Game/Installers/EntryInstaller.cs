using UnityEngine;
using Zenject;

namespace Main.Installers
{
    public class EntryInstaller : MonoInstaller<EntryInstaller>
    {
        [SerializeField] private WebViewObject webViewObject;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<MainValidator>()
                .AsSingle()
                .WithArguments(webViewObject)
                .NonLazy();
        }
    }
}