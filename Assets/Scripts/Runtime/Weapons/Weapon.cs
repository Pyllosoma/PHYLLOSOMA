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
        public float Range => _range;
        [Header("Weapon Base Info")]
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _range = 10f;
        [SerializeField] protected GameObject _owner = null;
        private void Awake(){
            _owner = transform.root.gameObject;
        }
        //사용 캐릭터 정보를 불러와야 할 수도 있음
        public abstract void Attack(GameObject target = null,Vector3 attackPoint = new Vector3());
        public abstract void Ready();
        public abstract void Finish();
        public T SetOwner(GameObject owner){
            _owner = owner;
            return this as T;
        }
        public bool IsInRange(float distance)
        {
            //Debug.Log(distance);
            return distance < _range;
        }
    }
}