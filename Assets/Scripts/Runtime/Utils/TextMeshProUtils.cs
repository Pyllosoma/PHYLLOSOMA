using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;

namespace Runtime.Utils
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProUtils : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _target;
        [SerializeField] private bool _isCapitalizeText = false;
        [SerializeField] private bool _isEnablePrefixAndSuffix = false;
        [SerializeField] private string _prefix = "";
        [SerializeField] private string _suffix = "";
        public void SetText(string text)
        {
            if (_isEnablePrefixAndSuffix) {
                text = _prefix + text + _suffix;
            }
            if (_isCapitalizeText) {
                text = text.ToUpper();
            }
            _target.text = text;
        }
        private void OnValidate()
        {
            _target = GetComponent<TextMeshProUGUI>();
        }
    }
}