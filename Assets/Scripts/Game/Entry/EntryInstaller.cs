using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Entry
{
    public class EntryInstaller : MonoInstaller<EntryInstaller>
    {
        [SerializeField] private Button startButton;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<EntryPresenter>()
                .AsSingle()
                .WithArguments(startButton)
                .NonLazy();
        }
    }
}