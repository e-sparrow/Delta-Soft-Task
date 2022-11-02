using Game.Entry;
using UnityEngine;
using UnityEngine.UI;
using Utils.WebView;
using Zenject;

namespace Game.Installers
{
    public class EntryInstaller : MonoInstaller<EntryInstaller>
    {
        [SerializeField] private Button startButton;
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
                .WithArguments(startButton)
                .NonLazy();
        }
    }
}