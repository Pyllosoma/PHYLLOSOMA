using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.UI.Components.TabBar
{
    public class IndexObjectActivator : MonoBehaviour
    {
        [SerializeField] private bool _deactivateOtherIndex = true;
        [SerializeField] private int _lastActivatedIndex = 0;
        [SerializeField] private List<GameObject> _gameObjects = new List<GameObject>();
        
        public void Activate(int index)
        {
            Debug.Log("Activate: " + index);
            //Return if index is out of range.
            if (index < 0 || index >= _gameObjects.Count) return;
            //Return if index is already activated.
            if (_deactivateOtherIndex) {
                _gameObjects[_lastActivatedIndex].SetActive(false);
                _gameObjects[index].SetActive(true);
            }
            else {
                _gameObjects[index].SetActive(true);
            }
            _lastActivatedIndex = index;
        }
    }
}