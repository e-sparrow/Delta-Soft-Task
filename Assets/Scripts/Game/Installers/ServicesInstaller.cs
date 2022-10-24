using Game.Core.Obstacles;
using Game.Services;
using Game.Services.Sound;
using Game.Services.Sound.Pitch;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(menuName = "Installers/Services", fileName = "ServicesInstaller")]
    public class ServicesInstaller : ScriptableObjectInstaller<ServicesInstaller>
    {
        [SerializeField] private SerializableSoundSettings soundSettings;
        [SerializeField] private SerializablePitchSettings pitchSettings;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<SerializableSoundSettings>()
                .FromInstance(soundSettings)
                .AsSingle();

            Container
                .BindInterfacesTo<SerializablePitchSettings>()
                .FromInstance(pitchSettings)
                .AsSingle();
            
            Container
                .BindInterfacesTo<PitchService>()
                .AsSingle();

            Container
                .BindInterfacesTo<SoundService>()
                .AsSingle();
        }
    }
}