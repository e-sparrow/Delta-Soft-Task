using Utils.Signals.Interfaces;

namespace Game.Core.Interfaces
{
    public interface IGameLoop
    {
        ISignal LevelLoaded
        {
            get;
        }

        ISignal TimerOver
        {
            get;
        }

        ISignal GameOver
        {
            get;
        }
    }
}