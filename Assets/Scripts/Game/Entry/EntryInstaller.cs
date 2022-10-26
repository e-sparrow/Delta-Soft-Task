using UnityEngine;
using UnityEngine.UI;
using VoltstroStudios.UnityWebBrowser.Core;
using Zenject;

namespace Game.Entry
{
    public class EntryInstaller : MonoInstaller<EntryInstaller>
    {
        [SerializeField] private BaseUwbClientManager webBrowser;
        [SerializeField] private GameObject webBrowserView;
        [SerializeField] private Button startButton;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<EntryPresenter>()
                .AsSingle()
                .WithArguments(webBrowser, webBrowserView, startButton)
                .NonLazy();
        }
    }
}