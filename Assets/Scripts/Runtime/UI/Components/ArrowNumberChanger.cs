using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Runtime.UI.Components
{
    public class ArrowNumberChanger : MonoBehaviour
    {
        public int Value => _value;
        [SerializeField] private int _min = 0;
        [SerializeField] private int _max = 10;
        [SerializeField] private int _value = 0;
        [SerializeField] private TextMeshProUGUI _numberText = null;
        [SerializeField] private Button _increaseButton = null;
        [SerializeField] private Button _decreaseButton = null;
        [SerializeField] private UnityEvent<int> _onNumberChanged = null;
        private Action<int> _onNumberChangedAction = null;
        public void Init(int min,int max,Action<int> onNumberChangedAction = null)
        {
            _min = min;
            _max = max;
            Reset();
            _onNumberChangedAction = onNumberChangedAction;
        }

        public void ChangeNumber(int value)
        {
            _value += value;
            _value = Mathf.Clamp(_value,_min,_max);
            _numberText.text = _value.ToString();
            _increaseButton.interactable = _value < _max;
            _decreaseButton.interactable = _value > _min;
            _onNumberChangedAction?.Invoke(_value);
            _onNumberChanged?.Invoke(_value);
        }
        private void OnEnable(){
            Reset();
        }
        public void Reset()
        {
            _value = _min;
            _numberText.text = _value.ToString();
            _increaseButton.interactable = _value < _max;
            _decreaseButton.interactable = _value > _min;
        }
    }
}