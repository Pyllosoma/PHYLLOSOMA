using System;
using UnityEngine;

namespace Runtime.Weapons
{
    /// <summary>
    /// 무기 최상위 클래스
    /// </summary>
    public abstract class Weapon<T> : MonoBehaviour where T : Weapon<T>
    {
        public float Damage => _damage;
        public float Delay => _delay / _attackSpeed;
        public float Duration => _duration / _attackSpeed;
        public float WaitTime => _waitTime / _attackSpeed;
        public float Range => _range;
        [Header("Weapon Base Info")]
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _delay = 0.05f;
        [SerializeField] private float _duration = 0.05f;
        [SerializeField] private float _waitTime = 0.05f;
        [SerializeField] private float _attackSpeed = 1f;
        [SerializeField] private float _range = 10f;
        [SerializeField] protected GameObject _owner = null;
        private void Awake(){
            _owner = transform.root.gameObject;
        }
        //사용 캐릭터 정보를 불러와야 할 수도 있음
        public abstract void Attack(GameObject target = null,Vector3 attackPoint = new Vector3());
        public abstract void Ready();
        public abstract void Finish();
        public T SetDelay(float delay){
            _delay = delay;
            return this as T;
        }
        public T SetOwner(GameObject owner){
            _owner = owner;
            return this as T;
        }
        public T SetDuration(float duration){
            _duration = duration;
            return this as T;
        }
        public T SetWaitTime(float waitTime){
            _waitTime = waitTime;
            return this as T;
        }
        public bool IsInRange(float distance)
        {
            //Debug.Log(distance);
            return distance < _range;
        }
    }
}