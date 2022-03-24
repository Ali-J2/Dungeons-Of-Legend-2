using System;
using System.Collections;
using UnityEngine;

namespace DungeonBrickStudios
{
    public class AnimationParticle : MonoBehaviour
    {
        [SerializeField] private string key = default;
        [SerializeField] private bool detachOnDestroy = default;
        [SerializeField] private float destroyTimerAfterDetach = default;
        [SerializeField] private bool lockRotationOnPlay = default;

        private ParticleSystem animationParticleSystem;
        private ParticleEventBridge particleEventBridge;
        private BaseAvatar baseAvatar;
        private Quaternion rotation;
        private Quaternion startRotation;
        private bool rotationLocked;

        private void Awake()
        {
            baseAvatar = GetComponentInParent<BaseAvatar>();
            animationParticleSystem = GetComponent<ParticleSystem>();
            particleEventBridge = GetComponentInParent<ParticleEventBridge>();
            particleEventBridge.onDestroyEvent += OnEvent_Destroy;
            startRotation = transform.localRotation;
            rotationLocked = false;
        }

        private void OnEnable()
        {
            particleEventBridge.onParticleEvent += OnEvent_Particle;
        }

        private void OnDisable()
        {
            particleEventBridge.onParticleEvent -= OnEvent_Particle;
        }

        private void Update()
        {
            if (!rotationLocked)
                return;

            transform.rotation = rotation;
        }

        private void OnEvent_Destroy()
        {
            if (!detachOnDestroy)
                return;
            
            transform.parent = null;
            GameObject.Destroy(gameObject, destroyTimerAfterDetach);
        }

        private void OnDestroy()
        {
            particleEventBridge.onDestroyEvent -= OnEvent_Destroy;
        }

        private void OnEvent_Particle(AnimationEvent animationEvent)
        {
            if (animationEvent.stringParameter != key)
                return;
            
            OnEvent_Particle();
        }
        

        private void OnEvent_Particle()
        {
            if (lockRotationOnPlay)
            {
                transform.localRotation = startRotation;
                rotation = transform.rotation;
                rotationLocked = true;
            }

            animationParticleSystem.Play(true);
        }
    }
}
