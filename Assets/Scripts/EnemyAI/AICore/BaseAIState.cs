using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonBrickStudios
{
    public class BaseAIState : MonoBehaviour
    {
        public bool updateAfterDeath;
        public virtual void OnEnterState() { }
        public virtual void OnStartState() { }
        public virtual void OnUpdateState() { }
        public virtual void OnFixedUpdateState() { }
        public virtual void OnExitState() { }
        public virtual void UpdateActionSpeed(float oldValue, float newValue) { }
    }
}
