using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Translation
{
    public class Translator : MonoBehaviour
    {
        [SerializeField] private string _translationKey = "NONE";
        [SerializeField] private Text _legacyText;
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            if (!TranslationManager.Instance) {
                Debug.LogError("Translator: TranslationManager is not found.");
                return;
            }
            if(_legacyText) _legacyText.text = TranslationManager.Instance.GetTranslation(_translationKey);
            if(_text) _text.text = TranslationManager.Instance.GetTranslation(_translationKey);
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
        }
    }
}