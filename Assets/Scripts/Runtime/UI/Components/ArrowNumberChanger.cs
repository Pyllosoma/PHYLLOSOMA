using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Runtime.UI.Components
{
    public class ArrowNumberChanger : MonoBehaviour
    {
        public int Value => _value;
        [SerializeField] private int _min = 0;
        [SerializeField] private int _max = 10;
        [SerializeField] private int _value = 0;
        [SerializeField] private TextMeshProUGUI _numberText = null;
        [SerializeField] private UnityEvent<int> _onNumberChanged = null;

        public void ChangeNumber(int value)
        {
            _value += value;
            _value = Mathf.Clamp(_value,_min,_max);
            _numberText.text = _value.ToString();
            _onNumberChanged?.Invoke(_value);
        }
        private void OnEnable(){
            Reset();
        }
        public void Reset()
        {
            _value = _min;
            _numberText.text = _value.ToString();
        }
    }
}