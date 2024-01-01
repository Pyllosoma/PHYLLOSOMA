using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tests.SaveSystem
{
    public class SaveObject : MonoBehaviour
    {
        private static Dictionary<string,SaveObject> _saveObjects = new Dictionary<string, SaveObject>();
        public static void LoadAll(List<string> saveKeys){
            foreach (var saveKey in saveKeys){
                if (_saveObjects.ContainsKey(saveKey)){
                    _saveObjects[saveKey]._onLoad?.Invoke();
                }
            }
        }
        public static void LoadAll(){
            var saveKeys = new List<string>();//TODO: SaveSystem.LoadAllKeys();
            LoadAll(saveKeys);
        }
        [SerializeField] private string _saveKey = "";
        [SerializeField] private UnityEvent _onLoad;
        private void Awake() {
            _saveObjects.Add(_saveKey,this);
        }
        private void OnDestroy() {
            _saveObjects.Remove(_saveKey);
        }
    }
}