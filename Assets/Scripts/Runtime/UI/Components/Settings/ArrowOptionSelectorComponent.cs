using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Runtime.UI.Components.Settings
{
    public class ArrowOptionSelectorComponent : SettingComponent<string>
    {
        [SerializeField] private int _currentSelectedIndex = 0;
        [SerializeField] private List<string> _values;
        [SerializeField] private List<LocalizedString> _valueTranslations;
        [SerializeField] private Button _leftArrowButton;
        [SerializeField] private Button _rightArrowButton;
        [SerializeField] private TextMeshProUGUI _valueLabel;
        protected override void OnEnable()
        {
            base.OnEnable();
            //Find index of current value.
            for (var i = 0; i < _values.Count; i++) {
                if (_values[i] != _settingValue) continue;
                _currentSelectedIndex = i;
                break;
            }
            //Run OnArrowButtonClick to update UI.
            OnArrowButtonClick(0);
        }

        public void OnArrowButtonClick(int index)
        {
            //Debug.Log("ArrowOptionSelector: OnArrowButtonClick");
            _currentSelectedIndex += index;
            if (_currentSelectedIndex < 0) _currentSelectedIndex = 0;
            if (_currentSelectedIndex >= _values.Count) _currentSelectedIndex = _values.Count - 1;
            //Disable arrow buttons when reach the end of the list.
            _leftArrowButton.interactable = _currentSelectedIndex != 0;
            _rightArrowButton.interactable = _currentSelectedIndex != _values.Count - 1;
            _valueLabel.text = _valueTranslations[_currentSelectedIndex].GetLocalizedString();
            
            //Save to setting id.
            _settingValue = _values[_currentSelectedIndex];
            SaveToSettingId();
        }
        protected override void OnValidate()
        {
            var buttons = GetComponentsInChildren<Button>();
            if (buttons.Length != 2) {
                Debug.LogError("ArrowOptionSelector: The number of buttons is not 2.");
                return;
            }
            _leftArrowButton = buttons[0];
            _rightArrowButton = buttons[1];
            _valueLabel = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
