using Game.UI.Counter.Interfaces;
using Zenject;

namespace Game.UI.Counter
{
    public class CounterPresenter : IInitializable, ILateDisposable
    {
        public CounterPresenter(ICounterModel model, ICounterView view)
        {
            _model = model;
            _view = view;
        }

        private readonly ICounterModel _model;
        private readonly ICounterView _view;
        
        public void Initialize()
        {
            _model.OnValueChanged += _view.SetCount;
        }

        public void LateDispose()
        {
            _model.OnValueChanged -= _view.SetCount;
        }
    }
}