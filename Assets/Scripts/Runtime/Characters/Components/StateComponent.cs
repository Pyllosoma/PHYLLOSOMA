using Runtime.Characters.FSM;
using UnityEngine;

namespace Runtime.Characters.Components
{
    public class StateComponent : Character
    {
        public GameObjectFSM State {
            get {
                return _state;
            }
            set {
                _state.Exit(gameObject);
                _state = value;
                _state.Enter(gameObject);
            }
        }
        [SerializeReference] private GameObjectFSM _state;
        private void Update() {
            _state?.Update(gameObject);
        }
        private void FixedUpdate() {
            _state?.FixedUpdate(gameObject);
        } 
        protected override void OnAlive()
        {
            _state?.Enter(gameObject);
        }
        protected override void OnDeath()
        {
            _state?.Exit(gameObject);
        }
    }
}