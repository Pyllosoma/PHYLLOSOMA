using System;
using System.Collections.Generic;
using Runtime.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Weapons
{
    /// <summary>
    /// 무기 최상위 클래스
    /// </summary>
    public abstract class Weapon : MonoBehaviour
    {
        public float Damage => _damage;
        public float Range => _range;
        [Header("Weapon Base Info")]
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _range = 10f;
        [TagSelector][SerializeField] protected List<string> _targetTags = new List<string>();
        [SerializeField] protected GameObject _owner = null;
        [SerializeField] protected UnityEvent _onReady;
        [SerializeField] protected UnityEvent _onAttack;
        [SerializeField] protected UnityEvent _onFinish;
        //Event Functions
        protected virtual void OnReady() { }

        protected virtual void OnAttack(GameObject target,Vector3 attackPoint){ }
        protected virtual void OnFinish(){ }
        public abstract bool IsUsable();
        public void Attack(GameObject target = null, Vector3 attackPoint = new Vector3()) {
            OnAttack(target,attackPoint);
            _onAttack.Invoke();
        }

        public void Ready() {
            OnReady();
            _onReady.Invoke();
        }

        public void Finish() {
            OnFinish();
            _onFinish.Invoke();
        }
        //사용 캐릭터 정보를 등록하고 사용할 수 있음
        public Weapon SetOwner(GameObject owner){
            _owner = owner;
            return this;
        }
        public bool IsInRange(float distance)
        {
            //Debug.Log(distance);
            return distance < _range;
        }
    }
}