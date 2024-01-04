using Runtime.Attributes;
using Runtime.Characters.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Weapons.Components
{
    public class CollideDamageComponent : MonoBehaviour
    {
        [SerializeField] private bool _isDestroyOnCollide = false;
        [Tooltip("If true, destroy this object when collide with target")]
        [ShowIf("_isDestroyOnCollide")] [SerializeField] private bool _isDestroyOnTarget = false;
        [SerializeField] private float _damage = 10f;
        [TagSelector][SerializeField] private string _targetTag = "Enemy";

        private void OnCollisionEnter(Collision other)
        {
            GiveDamage(other.gameObject);
        }
        private void OnTriggerEnter(Collider other)
        {
            GiveDamage(other.gameObject);
        }
        private void GiveDamage(GameObject target)
        {
            if (!target.CompareTag(_targetTag)) {
                if (_isDestroyOnCollide&&!_isDestroyOnTarget) Destroy(gameObject);
                return;
            }
            var healthComponent = target.gameObject.GetComponent<HealthComponent>();
            if (healthComponent) healthComponent.GiveDamage(_damage);
            if (_isDestroyOnCollide) Destroy(gameObject);
        }
        
    }
}