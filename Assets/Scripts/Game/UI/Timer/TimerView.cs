using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game.UI.Timer.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Timer
{
    public class TimerView : MonoBehaviour, ITimerView
    {
        public event Action OnTimerOver = () => { };
        
        [SerializeField] private Image panel;
        [SerializeField] private TMP_Text text;

        private TweenerCore<float, float, FloatOptions> _tweener;

        public void SetValue(float value)
        {
            text.text = value.
                ToString("0.#")
                .Replace(',', '.');
        }
        
        public void StartTimer(float time)
        {
            panel.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            
            float value = time;

            _tweener?.Kill();
            _tweener = DOTween.To(GetValue, SetValue, 0f, time).SetEase(Ease.Linear)
                .OnPlay(() => panel.rectTransform.DOSizeDelta(Vector2.zero, time / 2)
                .OnComplete(() => text.transform.DOShakePosition(time / 2)))
                .OnComplete(Complete);

            float GetValue()
            {
                return value;
            }

            void SetValue(float newValue)
            {
                value = newValue;
                text.text = value
                    .ToString("0.#")
                    .Replace(',', '.');
            }

            void Complete()
            {
                OnTimerOver.Invoke();
                
                panel.gameObject.SetActive(false);
                text.gameObject.SetActive(false);
            }
        }
    }
}
