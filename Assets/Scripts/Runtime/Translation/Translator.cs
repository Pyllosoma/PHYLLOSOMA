using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Translation
{
    public class Translator : MonoBehaviour,ITranslationObserver
    {
        [SerializeField] private bool _enablePreload = true;
        [SerializeField] private string _translationKey = "NONE";
        [SerializeField] private string _frontText = "";
        [SerializeField] private string _backText = "";
        [SerializeField] private Text _legacyText;
        [SerializeField] private TextMeshProUGUI _text;
        private void Awake() {
            //TranslationManager.AddObserver(this);
        }
        private void OnEnable()
        {
            InternalLoadTranslation();
        }
        private void InternalLoadTranslation()
        {
            if(_legacyText) _legacyText.text = _frontText + TranslationManager.GetTranslation(_translationKey) + _backText;
            if(_text) _text.text = _frontText + TranslationManager.GetTranslation(_translationKey) + _backText;
        }
        private void OnValidate()
        {
            var legacy = GetComponent<Text>();
            var tmp = GetComponent<TextMeshProUGUI>();
            if (legacy != null) {
                _legacyText = legacy;
            }
            else if (tmp != null) {
                _text = tmp;
            }
            else {
                Debug.LogError("Translator: Text or TextMeshProUGUI component is not found.");
            }

            if (_enablePreload) {
                InternalLoadTranslation();
            }
        }
        //Invoke when translation changed.
        public void OnTranslationChanged()
        {
            //If gameobject is not active, do nothing.
            if (!gameObject.activeSelf) return;
            //If translation key is NONE, do nothing.
            if (_translationKey == "NONE") return;
            InternalLoadTranslation();
        }
    }
}