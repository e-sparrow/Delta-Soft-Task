using Game.UI.Counter.Interfaces;
using TMPro;
using UnityEngine;

namespace Game.UI.Counter
{
    public class CounterView : MonoBehaviour, ICounterView
    {
        [SerializeField] private TMP_Text text;
        
        public void SetCount(int count)
        {
            text.text = count.ToString();
        }
    }
}