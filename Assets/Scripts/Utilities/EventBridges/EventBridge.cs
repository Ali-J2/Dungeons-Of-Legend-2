using System;
using UnityEngine;

namespace DungeonBrickStudios
{
    public abstract class EventBridge<T> : MonoBehaviour where T : struct, IConvertible
    {
        public event Action<T> onAnimationEvent;

        // This event is called by the Animator
        private void OnEvent_Animation(string eventName)
        {
            if (onAnimationEvent == null)
                return;

            T animationEvent = eventName.GetIdentifierEnum<T>();
            onAnimationEvent(animationEvent);
        }
    }
}
