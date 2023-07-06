using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Runtime.Translation
{
    //단하나만 존재하며 항상 메모리 공간에 위치해야 하기 때문에 static으로 선언
    public static class TranslationManager
    {
        private const string TRANSLATION_PATH = "Translations/";
        private const string UI_TRANSLATION_PATH = "ui";
        private const string GAME_ITEM_TRANSLATION_PATH = "item";
        //현재 언어를 저장하는 변수
        private static string _currentLanguage = "ko_kr";
        //데이터를 저장하는 변수
        private static readonly Dictionary<string, string> _translation = new Dictionary<string, string>();
        public static void InitLanguage(string language)
        {
            _currentLanguage = language;
            InternalLoadTranslation();
        }
        private static void InternalLoadTranslation()
        {
            _translation.Clear();
            var translationPath = TRANSLATION_PATH + _currentLanguage + "/";
            var uiTranslationPath = translationPath + UI_TRANSLATION_PATH;
            var gameItemTranslationPath = translationPath + GAME_ITEM_TRANSLATION_PATH;
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
            //Load Game Item Translation
            var gameItemTranslation = Resources.Load<TextAsset>(gameItemTranslationPath);
            //Debug.Log(gameItemTranslation.text);
            if (gameItemTranslation == null) {
                Debug.LogError("TranslationManager: Game Item Translation is not found.");
                return;
            }
            var gameItemTranslationData = JsonConvert.DeserializeObject<Dictionary<string, string>>(gameItemTranslation.text);
            foreach (var data in gameItemTranslationData) {
                _translation.Add(data.Key, data.Value);
            }
        }
        /// <summary>
        /// Get translation from translation dictionary.
        /// </summary>
        /// <param name="translationKey">The key that matches translation string.</param>
        /// <returns>Translation string founded by key</returns>
        public static string GetTranslation(string translationKey) {
            if (_translation.Count == 0) {
                InternalLoadTranslation();
            }
            return !_translation.ContainsKey(translationKey) ? translationKey : _translation[translationKey];
        }
    }
}