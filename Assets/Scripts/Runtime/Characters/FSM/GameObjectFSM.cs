using System;
using Runtime.Patterns.FSM;
using UnityEngine;

namespace Runtime.Characters.FSM
{
    [Serializable]
    public class GameObjectFSM : IState<GameObject>
    {
        public virtual void Enter(GameObject entity) {}
        public virtual void Update(GameObject entity){}
        public virtual void FixedUpdate(GameObject entity){}
        public virtual void Exit(GameObject entity) {}
    }
}