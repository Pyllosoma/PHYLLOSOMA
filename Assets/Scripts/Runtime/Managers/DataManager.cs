using System;
using Newtonsoft.Json;
using Runtime.Data;
using Runtime.Data.ScriptableObjects;
using Runtime.Patterns;
using UnityEngine;

namespace Runtime.Managers
{
    public class DataManager : Singleton<DataManager>
    {
        public enum SaveDataType {
            NEW_GAME,//Create new save data
            LOAD_GAME,//Load save from file list
            CONTINUE_GAME//Load last save data
        }
        public const string PLAYER_SAVE_PATH = "save.data";
        public PlayerData PlayerData => _playerData;
        public ItemInfos Items => _itemInfos;
        [SerializeField] private ItemInfos _itemInfos;
        [SerializeField] private PlayerData _playerData = new PlayerData();
        private long _playStartTime = 0;
        private long _playEndTime = 0;
        public void SaveDataToPath<T>(string path,T data)
        {
            string savePath = Application.persistentDataPath + "\\" + path;
            Debug.Log(savePath);
            System.IO.File.WriteAllText(savePath,JsonConvert.SerializeObject(data));
        }
        public T LoadDataFromPath<T>(string path)
        {
            string loadPath = Application.persistentDataPath + "\\" + path;
            if (!System.IO.File.Exists(loadPath)) {
                Debug.Log("Save file is not exists.");
                return default;
            }
            
            string jsonData = System.IO.File.ReadAllText(loadPath);
            return JsonConvert.DeserializeObject<T>(jsonData) ?? default;
        }
        public void Load() {
            _playerData = LoadDataFromPath<PlayerData>(PLAYER_SAVE_PATH);
            if (_playerData == null) {
                _playerData = new PlayerData();
            }
            _playStartTime = DateTime.Now.Ticks;
        }
        public void Save()
        {
            _playEndTime = DateTime.Now.Ticks;
            _playerData.LastSaveTime = DateTime.Now.Ticks;
            _playerData.TotalPlayTime += _playEndTime - _playStartTime;
            SaveDataToPath(PLAYER_SAVE_PATH,_playerData);
        }
        public void LoadSaveData(SaveDataType saveDataType)
        {
            switch (saveDataType) {
                case SaveDataType.NEW_GAME:
                    _playerData = new PlayerData();
                    _playStartTime = DateTime.Now.Ticks;
                    break;
                case SaveDataType.LOAD_GAME:
                    _playerData = LoadDataFromPath<PlayerData>(PLAYER_SAVE_PATH);
                    if (_playerData == null) {
                        _playerData = new PlayerData();
                    }
                    _playStartTime = DateTime.Now.Ticks;
                    break;
                default:
                    break;
            }
        }
    }
}