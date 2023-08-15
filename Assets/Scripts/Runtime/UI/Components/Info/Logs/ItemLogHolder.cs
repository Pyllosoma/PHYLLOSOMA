using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.UI.Components.Info.Logs
{
    public class ItemLogHolder : MonoBehaviour
    {
        [SerializeField] private Transform _itemLogParent;
        [SerializeField] private ItemLog _itemLogPrefab;
        [Header("Log Settings")]
        [SerializeField] private int _maxLogCount = 5;
        [SerializeField] private float _logDuration = 1f;
        [SerializeField] private int _logUpdatePerSecond = 10;
        private Queue<KeyValuePair<int,int>> _logQueue = new Queue<KeyValuePair<int, int>>();
        private void OnEnable()
        {
            StartCoroutine(UpdateLog());
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                CreateLog(new KeyValuePair<int, int>(0,1000));
            }
        }
        public void CreateLog(KeyValuePair<int,int> value)
        {
            if (value.Value == 0 ) return;
            _logQueue.Enqueue(value);
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
                var data = _logQueue.Dequeue();
                var logGo = Instantiate(_itemLogPrefab, _itemLogParent).GetComponent<ItemLog>();
                logGo.InitItemLog(data,true,curLogCount * _logDuration, () => {
                    curLogCount--;
                });
            }
        }
    }
}