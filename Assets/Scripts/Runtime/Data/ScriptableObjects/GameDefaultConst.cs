using System;
using UnityEngine;

namespace Runtime.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameDefaultConst", menuName = "Data/GameDefaultConst")]
    public class GameDefaultConst : SingletonScriptableObject<GameDefaultConst>
    {
        [SerializeField] private int _baseCost = 510;
        [SerializeField] private int _startAnimaCost = 10;
        [SerializeField] private int _startAnimaRaiseCost = 90;
        [SerializeField] private int _animaIncreasePerLevel = 8;
        [SerializeField] private int _costAnimaRaiseLevel = 10;
        [Range(0, 100)] public int Test = 1;
        public int CalculateCostPerLevel(int level)
        {
            int currentCost = _baseCost + _startAnimaCost;
            int currentRaiseCost = _startAnimaCost + _animaIncreasePerLevel;
            for (int i = 0; i < level; i++)
            {
                if (i < _costAnimaRaiseLevel) {
                    currentCost += currentRaiseCost;
                    continue;
                }
                if (i == _costAnimaRaiseLevel)
                    currentRaiseCost += _startAnimaRaiseCost;
                else
                    currentRaiseCost += _animaIncreasePerLevel;
                currentCost += currentRaiseCost;
            }
            return currentCost;
        }

        public int CalculateCostPerLevel(int currentLevel, int targetLevel)
        {
            int result = 0;
            for (int i = currentLevel; i < targetLevel; i++) {
                result += CalculateCostPerLevel(i);
            }
            return result;
        }
        private void OnValidate()
        {
            Debug.Log(CalculateCostPerLevel(Test));
        }
    }
}