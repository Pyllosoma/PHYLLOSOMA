using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Runtime.Translation
{
    public class TranslationManager : MonoBehaviour
    {
        private const string TRANSLATION_PATH = "Translations/";
        private const string UI_TRANSLATION_PATH = "ui";
        private string _currentLanguage = "ko_kr";
        //데이터를 저장하는 변수
        private readonly Dictionary<string, string> _translation = new Dictionary<string, string>();
        public static TranslationManager Instance { get; private set; }
        
        private void Awake()
        {
            if (!Instance) {
                Instance = this;
                SetLanguage("ko_kr");
                DontDestroyOnLoad(transform.root.gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }

        public void SetLanguage(string language)
        {
            _currentLanguage = language;
            InternalLoadTranslation();
        }
        private void InternalLoadTranslation()
        {
            _translation.Clear();
            var translationPath = TRANSLATION_PATH + _currentLanguage + "/";
            var uiTranslationPath = translationPath + UI_TRANSLATION_PATH;
            
            //Load UI Translation
            var uiTranslation = Resources.Load<TextAsset>(uiTranslationPath);
            //Debug.Log(uiTranslation.text);
            if (uiTranslation == null) {
                Debug.LogError("TranslationManager: UI Translation is not found.");
                return;
            }
            var uiTranslationData = JsonConvert.DeserializeObject<Dictionary<string, string>>(uiTranslation.text);
            foreach (var data in uiTranslationData) {
                _translation.Add(data.Key, data.Value);
            }
        }
        /// <summary>
        /// Get translation from translation dictionary.
        /// </summary>
        /// <param name="translationKey">The key that matches translation string.</param>
        /// <returns>Translation string founded by key</returns>
        public string GetTranslation(string translationKey) {
            return !_translation.ContainsKey(translationKey) ? translationKey : _translation[translationKey];
        }
    }
}