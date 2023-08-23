using System;
using System.Collections.Generic;
using Runtime.Attributes;
using UnityEngine;

namespace Runtime.Weapons
{
    [RequireComponent(typeof(Collider))]
    public class MeleeWeapon : Weapon<MeleeWeapon>
    {
        [TagSelector][SerializeField] protected List<string> _targetTags = new List<string>();
        [SerializeField] protected Collider _collider;
        private void Start()
        {
            _collider.enabled = false;
        }
        public override void Ready()
        {
            //공격 준비시 필요한 행동
            //ex) 애니메이션 속도 설정
        }
        public override void Attack(GameObject target = null, Vector3 attackPoint = new Vector3())
        {
            //공격 시간에 따라 공격 활성화
            Debug.Log("MeleeWeapon Attack!");
        }
        private void OnTriggerEnter(Collider other)
        {
            if (_targetTags.Contains(other.tag)) {
                Attack(other.gameObject);
            }
        }
        public override void Finish(){
            _collider.enabled = false;
        }
    }
}