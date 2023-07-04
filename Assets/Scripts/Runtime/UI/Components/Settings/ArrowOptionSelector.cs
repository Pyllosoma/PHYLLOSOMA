using System.Collections.Generic;
using Runtime.Translation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Components.Settings
{
    public class ArrowOptionSelector : SettingComponent<string>
    {
        [SerializeField] private int _currentSelectedIndex = 0;
        [SerializeField] private List<string> _values;
        [SerializeField] private List<string> _valueTranslations;
        [SerializeField] private Button _leftArrowButton;
        [SerializeField] private Button _rightArrowButton;
        [SerializeField] private TextMeshProUGUI _valueLabel;
        protected override void OnEnable()
        {
            base.OnEnable();
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
            _valueLabel.text = TranslationManager.GetTranslation(_valueTranslations[_currentSelectedIndex]);
        }
        private void OnValidate()
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
