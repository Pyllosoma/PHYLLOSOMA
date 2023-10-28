﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Runtime.Patterns;
using Runtime.Settings;
using UnityEngine;
using UnityEngine.Audio;

namespace Runtime.Managers
{
    public class SettingManager : Singleton<SettingManager>
    {
        private const string _settingSavePath = "setting.data";
        private Dictionary<string, string> _settingData = null;
        [SerializeField] private AudioMixer _mixer;
        //The setting value will apply by real time.
        public override void Awake()
        {
            base.Awake();
            LoadSettingData();
            ApplySettingData();
        }
        private void LoadSettingData()
        {
            _settingData = new Dictionary<string, string>();
            var path = Application.persistentDataPath + "\\" + _settingSavePath;
            //Debug.Log(path);
            //Check is file exists.
            if (!File.Exists(path)) {
                Debug.Log("SettingManager: Setting file is not exists.");
                //Set as default setting.
                _settingData.Add(SettingConstant.SCREEN_RESOLUTION, "1920X1080");
                _settingData.Add(SettingConstant.MASTER_VOLUME, "1");
                _settingData.Add(SettingConstant.BGM_VOLUME, "1");
                _settingData.Add(SettingConstant.SFX_VOLUME, "1");
                _settingData.Add(SettingConstant.CONVERSATION_VOLUME, "1");
                _settingData.Add(SettingConstant.LANGUAGE, "ko_kr");
                _settingData.Add(SettingConstant.SHOW_TUTORIAL, "true");
                _settingData.Add(SettingConstant.SENSITIVITY, "1");
                //and save it.
                SaveSettingData();
                return;
            }
            //Get setting data from game default path.
            var jsonData = File.ReadAllText(path);
            //Load setting data from json file.
            _settingData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
        }
        private void SaveSettingData()
        {
            var path = Application.persistentDataPath + "\\" + _settingSavePath;
            //Save setting data to game default path.
            var jsonData = JsonConvert.SerializeObject(_settingData);
            //Debug.Log(jsonData);
            File.WriteAllText(path, jsonData);
            ApplySettingData();
        }
        private void ApplySettingData()
        {
            //Debug.Log("ApplySettingData");
            // Debug.Log(_settingData[SettingConstant.MASTER_VOLUME]);
            // Debug.Log(_settingData[SettingConstant.BGM_VOLUME]);
            // Debug.Log(_settingData[SettingConstant.SFX_VOLUME]);
            // Debug.Log(_settingData[SettingConstant.CONVERSATION_VOLUME]);
            // Debug.Log(_settingData[SettingConstant.SCREEN_RESOLUTION]);
            string resolution = _settingData[SettingConstant.SCREEN_RESOLUTION];
            string[] res = resolution.Split('X');
            Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), true);
            _mixer.SetFloat("Master", float.Parse(_settingData[SettingConstant.MASTER_VOLUME]) * 100 - 90);
            _mixer.SetFloat("SFX", float.Parse(_settingData[SettingConstant.BGM_VOLUME]) * 100 - 90);
            _mixer.SetFloat("BGM", float.Parse(_settingData[SettingConstant.SFX_VOLUME]) * 100 - 90);
            _mixer.SetFloat("Conversation", float.Parse(_settingData[SettingConstant.CONVERSATION_VOLUME]) * 100 - 90);
        }
        public T GetSettingDataById<T>(string settingId)
        {
            //check is setting id exists.
            if (!_settingData.ContainsKey(settingId)) {
                Debug.LogError("Setting id not found.");
                return default;
            }
            //Get setting value by setting id.
            var settingValue = _settingData[settingId];
            //Convert setting value to generic type.
            try {
                var convertedValue = (T)Convert.ChangeType(settingValue, typeof(T));
                return convertedValue;
            }
            catch (Exception e) {
                Debug.LogError($"Setting value convert error.\n{e}");
                return default;
            }
            
        }
        /// <summary>
        /// Save setting data by setting id.
        /// </summary>
        /// <param name="settingId">The dictionary id of setting data</param>
        /// <param name="settingData">The value of setting data</param>
        public void SetSettingDataById(string settingId, string settingData)
        {
            if (_settingData == null) return;
            //Save setting value by setting id.
            _settingData[settingId] = settingData;
            //Save setting data to game default path.
            SaveSettingData();
        }
    }
}