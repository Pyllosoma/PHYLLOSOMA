using System;
using UnityEngine;

namespace Runtime.Weapons
{
    public class MeleeWeapon : Weapon<MeleeWeapon>
    {
        [SerializeField] protected Animator _animator;
        [SerializeField] protected Collider _collider;
        [SerializeField] protected float _attackAnimationSpeed = 1f;
        private void Start()
        {
            _collider.enabled = false;
        }
        public override void Ready()
        {
            //공격 준비시 필요한 행동
            //ex) 애니메이션 속도 설정
            _collider.enabled = true;
            _animator.speed = _attackAnimationSpeed;
        }
        public override void Attack(GameObject target, Vector3 attackPoint)
        {
            //공격하는 부분
            //Debug.Log("Melee Attack!");
            //공격할 타겟에게 
            //Calculate hit point
            // shot raycast
            //공격 데미지를 주는 부분
            //Debug.Log("Attack target : " + target.name);
            
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("MeleeWeapon : " + other.name + " : OnTriggerEnter");
        }
        public override void Finish()
        {
            _collider.enabled = false;
            _animator.speed = 1f;
        }
    }
}