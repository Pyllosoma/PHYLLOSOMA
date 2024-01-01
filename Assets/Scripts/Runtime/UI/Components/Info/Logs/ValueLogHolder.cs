using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.UI.Components.Info.Logs
{
    public class ValueLogHolder : MonoBehaviour
    {
        [SerializeField] private Transform _valueLogParent;
        [SerializeField] private ValueLog _valueLogPrefab;
        [Header("Log Settings")]
        [SerializeField] private int _maxLogCount = 5;
        [SerializeField] private float _logDuration = 1f;
        [SerializeField] private int _logUpdatePerSecond = 10;
        private Queue<string> _logQueue = new Queue<string>();
        private void OnEnable()
        {
            StartCoroutine(UpdateLog());
        }
        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.Space)) {
        //         CreateLog(1000);
        //     }
        // }
        public void CreateLog(int value)
        {
            if (value == 0 ) return;
            char prefix = value > 0 ? '+' : '-';
            _logQueue.Enqueue($"{prefix}{value}");
        }
        private IEnumerator UpdateLog()
        {
            float updateRatio = 1f / _logUpdatePerSecond;
            int curLogCount = 0;
            while (gameObject.activeSelf) {
                yield return new WaitForSeconds(updateRatio);
                if (_logQueue.Count == 0) {
                    continue;
                }
                if (curLogCount >= _maxLogCount) {
                    continue;
                }
                curLogCount++;
                var curMessage = _logQueue.Dequeue();
                var logGo = Instantiate(_valueLogPrefab, _valueLogParent).GetComponent<ValueLog>();
                logGo.InitLog(curMessage,true,curLogCount * _logDuration, () => {
                    curLogCount--;
                });
            }
        }
    }
}