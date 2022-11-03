using Game.Entry;
using UnityEngine;
using Utils.WebView;
using Zenject;

namespace Game.Installers
{
    public class EntryInstaller : MonoInstaller<EntryInstaller>
    {
        [SerializeField] private MonoWebViewController webViewController;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<MonoWebViewController>()
                .FromInstance(webViewController)
                .AsSingle();
            
            Container
                .BindInterfacesTo<EntryPresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}