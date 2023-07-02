using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.UI.Components.TabBar
{
    /// <summary>
    /// TabBarManager is a component that manages the tab bar.
    /// When a tab is selected, The tab that selected invokes the OnTabSelected method of the TabBarManager.
    /// And Activate the tab page that is passed as an argument.
    /// </summary>
    public class TabBarManager : MonoBehaviour
    {
        [SerializeField] private bool _isInitializeOnEnabled = true;
        [SerializeField] private int _startSelectedIndex = 0;
        [SerializeField] private int _currentSelectedIndex = 0;
        [SerializeField] private List<TabButton> _tabButtons = new List<TabButton>();
        [SerializeField] private UnityEvent<int> _onTabSelected;
        private void OnEnable()
        {
            if (_tabButtons.Count == 0) return;
            if (!_isInitializeOnEnabled) return;
            OnTabSelected(_startSelectedIndex);
        }
        /// <summary>
        /// Activate selected button and deactivate other buttons.
        /// and invoke the OnTabSelected event.
        /// </summary>
        /// <param name="index">The index of selected button</param>
        public void OnTabSelected(int index)
        {
            Debug.Log($"OnTabSelected: {index}");
            _currentSelectedIndex = index;
            for (var i = 0; i < _tabButtons.Count; i++) {
                _tabButtons[i].Attention(i == index);
            }
            _onTabSelected.Invoke(_currentSelectedIndex);
        }
        private void OnValidate()
        {
            _tabButtons.Clear();
            var tabButtons = GetComponentsInChildren<TabButton>();
            for (var i = 0; i < tabButtons.Length; i++)
            {
                tabButtons[i].Index = i;
                tabButtons[i].Parent = this;
                _tabButtons.Add(tabButtons[i]);
            }
        }
    }
}