using System;
using UnityEngine;

namespace Tests.Weapons
{
    //본래는 무기 클래스 구현 후 상속 받은 뒤에 구현되어야 하지만 테스트를 위해 빈 클래스로 구현
    public class Laser : MonoBehaviour
    {
        public float Damage => _damage;
        public float Range => _attackRange;
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _attackDelay = 0.05f;
        [SerializeField] private float _attackRange = 10f;
        [SerializeField] private float _attackAngle = 45f;
        [SerializeField] private LineRenderer _laserLine = null;
        private float _lastAttackTime = 0f;
        public void Attack(GameObject target)
        {
            if (Time.time - _lastAttackTime < _attackDelay) return;
            _lastAttackTime = Time.time;
            //공격하는 부분
            Debug.Log("Laser Attack!");
            //공격할 타겟에게 
            _laserLine.SetPositions(new []{transform.position, target.transform.position});
        }
        public bool IsInRange(GameObject target)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > _attackRange) {
                return false;
            }
            return true;
        }
        public bool IsInAttackAngle(GameObject target)
        {
            var direction = target.transform.position - transform.position;
            var angle = Vector3.Angle(direction, transform.forward);
            if (angle > _attackAngle) {
                return false;
            }
            return true;
        }
    }
}