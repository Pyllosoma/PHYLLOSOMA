using System;
using Runtime.Managers;
using Runtime.UI.Components.Info.Indicators;
using Runtime.UI.Components.Info.Logs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.UI
{
    public class AnimaIndicateWindow : UIWindow
    {
        public static AnimaIndicateWindow Instance { get; private set; } = null;
        [SerializeField] private ValueIndicator _valueIndicator;
        [SerializeField] private ValueLogHolder _valueLogHolder;
        public AnimaIndicateWindow() : base() {
            Instance = this;
        }
        protected override void Awake()
        {
            base.Awake();
            var startAnima = DataManager.Instance.PlayerData.Anima;
            _valueIndicator.SetStartValue(startAnima);
        }
        // public void Update()
        // {
        //     if (Keyboard.current[Key.Space].wasPressedThisFrame) {
        //         DataManager.Instance.PlayerData.Anima += 1000;
        //         ShowAnimaChange(1000);
        //     }
        // }
        public void ShowAnimaChange(int change)
        {
            _valueIndicator.UpdateValue(DataManager.Instance.PlayerData.Anima);
            _valueLogHolder.CreateLog(change);
        }
    }
}