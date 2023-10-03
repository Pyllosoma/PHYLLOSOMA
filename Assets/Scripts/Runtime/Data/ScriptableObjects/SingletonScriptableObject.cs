using UnityEngine;

namespace Runtime.Data.ScriptableObjects
{
    public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        public const string DEFAULT_PATH = "Data";
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance) return _instance;
                if (!Load()) {
                    Debug.LogError($"Failed to load {typeof(T)}.");
                }
                return _instance;
            }
        }
        public static bool Load()
        {
            var loadedData = Resources.LoadAll<T>(DEFAULT_PATH);
            if (loadedData.Length == 0) {
                Debug.LogError($"No {typeof(T)} found in {DEFAULT_PATH}.");
                return false;
            }

            if (loadedData.Length > 1) {
                Debug.LogError($"More than one {typeof(T)} found in {DEFAULT_PATH}.");
            }
            _instance = loadedData[0];
            return true;
        }
    }
}