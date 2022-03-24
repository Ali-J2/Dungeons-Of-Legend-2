using System;
using UnityEngine;

namespace DungeonBrickStudios
{
    public class ParticleEventBridge : MonoBehaviour
    {
        public event Action<AnimationEvent> onParticleEvent;
        public event Action onDestroyEvent;

        private void OnEvent_Particle(AnimationEvent myEvent)
        {
            if (onParticleEvent != null)
                onParticleEvent(myEvent);
        }

        public void PreDestroy()
        {
            if (onDestroyEvent != null)
                onDestroyEvent();
        }
    }
}
