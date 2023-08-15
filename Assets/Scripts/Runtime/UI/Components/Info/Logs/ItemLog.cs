using System;
using System.Collections.Generic;
using Runtime.UI.Components.Animations;
using Runtime.Utils;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Info.Logs
{
    public class ItemLog : MonoBehaviour
    {
        [SerializeField] private float _logLifeTime = 1f;
        [SerializeField] private TextMeshProUGUI _itemNameText;
        [SerializeField] private TextMeshProUtils _itemCountText;
        [SerializeField] private UIAnimation _moveAnimation;
        [SerializeField] private UIAnimation _fadeAnimation;
        [SerializeField] private UIAnimation _resizeAnimation;
        private Action _onComplete = null;
        public void InitItemLog(KeyValuePair<int,int> itemInfo,bool disappearAuto = false , float logLifeTime = 1f , Action onComplete = null)
        {
            //Need to make item name importer later
            _itemNameText.SetText("아이템 이름");
            _itemCountText.SetText(itemInfo.Value.ToString());
            _logLifeTime = logLifeTime;
            _onComplete = onComplete;
            _fadeAnimation.Play();
            _moveAnimation.Play(() => {
                if (!disappearAuto) return;
                Invoke(nameof(DisappearLog),_logLifeTime);
            });
        }
        
        public void DisappearLog()
        {
            _fadeAnimation.Rewind();
            _moveAnimation.Rewind(() => {
                
                _resizeAnimation.Play(() => {
                    _onComplete?.Invoke();
                    Destroy(gameObject);
                });
            });
        }
        
    }
}