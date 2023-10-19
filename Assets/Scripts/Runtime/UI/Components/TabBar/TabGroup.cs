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
    public class TabGroup : MonoBehaviour
    {
        public int SelectedIndex {
            get {
                return _selectedIndex;
            }
            private set {
                _selectedIndex = value;
                _selectedIndex = Mathf.Clamp(_selectedIndex, 0, _tabButtons.Length - 1);
            }
        }
        [SerializeField] private int _startUpIndex = 0;
        [SerializeField] private TabButton[] _tabButtons;
        [SerializeField] private UnityEvent<int> _onTabSelected;
        private int _selectedIndex = -1;
        private void OnEnable()
        {
            OnTabSelected(_startUpIndex);
        }
        /// <summary>
        /// Activate selected button and deactivate other buttons.
        /// and invoke the OnTabSelected event.
        /// </summary>
        /// <param name="index">The index of selected button</param>
        public void OnTabSelected(int index)
        {
            if (SelectedIndex == index) return;
            foreach (var tabButton in _tabButtons) {
                if (tabButton.Index == index) {
                    tabButton.Toggle();
                }
                else {
                    tabButton.UnToggle();
                }
            }
            SelectedIndex = index;
            _onTabSelected?.Invoke(index);
            #if UNITY_EDITOR
                        Debug.Log(name + " : Selected index : " + index);
            #endif
        }
        private void OnValidate()
        {
            _tabButtons = GetComponentsInChildren<TabButton>();
            for (var i = 0; i < _tabButtons.Length; i++)
            {
                _tabButtons[i].Index = i;
                _tabButtons[i].Parent = this;
            }
        }
    }
}