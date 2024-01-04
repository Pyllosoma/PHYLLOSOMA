using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Characters.FSM
{
    [Serializable]
    public class ModuleGameObjectState : GameObjectFSM
    {
        [SerializeReference] private List<GameObjectFSM> _states = new List<GameObjectFSM>();
        public override void Enter(GameObject entity) => _states.ForEach(state => state.Enter(entity));
        public override void Update(GameObject entity) => _states.ForEach(state => state.Update(entity));
        public override void FixedUpdate(GameObject entity) => _states.ForEach(state => state.FixedUpdate(entity));
        public override void Exit(GameObject entity) => _states.ForEach(state => state.Exit(entity));
    }
}