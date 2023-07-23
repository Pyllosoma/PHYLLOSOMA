﻿using System;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Settings
{
    public class DropDownDisplaySettingComponent : SettingComponent<string>
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        private void Awake()
        {
            _dropdown.ClearOptions();
            //Get display resolution and add to dropdown.
            var resolutions = Screen.resolutions;
            foreach (var resolution in resolutions)
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
                optionData.text = resolution.width + "X" + resolution.height;
                _dropdown.options.Add(optionData);
            }
            _dropdown.RefreshShownValue();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            //Set dropdown value to current resolution.
            var currentResolution = _settingValue;
            //Debug.Log(currentResolution);
            for (int i = 0; i < _dropdown.options.Count; i++) {
                if (_dropdown.options[i].text != currentResolution) continue;
                _dropdown.value = i;
            }
        }
        public override void SaveToSettingId()
        {
            //Set setting value and save.
            _settingValue = _dropdown.options[_dropdown.value].text;
            Debug.Log(_settingValue);
            base.SaveToSettingId();
        }
        
        protected override void OnValidate()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
        }
    }
}