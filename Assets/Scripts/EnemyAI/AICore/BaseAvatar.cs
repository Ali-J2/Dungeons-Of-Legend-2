using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SheetCodes;

namespace DungeonBrickStudios
{
    public enum DamageResult { Success, Blocked, Immune }
    public abstract class BaseAvatar : MonoBehaviour
    {
        public event Action<BaseAvatar> onDeath;
        public event Action<BaseAvatar, int> onTakeDamage;
        public event Action<BaseAvatar> onTurnComplete;

        public readonly ControlledEventVariable<BaseAvatar, int> health;
        public readonly EventVariable<BaseAvatar, int> maxHealth;
        public readonly EventVariable<BaseAvatar, float> actionSpeed;
        public readonly EventVariable<BaseAvatar, bool> isInCombat;
        public readonly EventVariable<BaseAvatar, bool> isInvulnerable;
        public readonly EventVariable<BaseAvatar, float> invulnerableDurationLeft;

        public readonly EventList<float> actionSpeedMultipliers;
        [NonSerialized] public bool isDead;

        public float actionDeltaTime => actionSpeed.value * Time.deltaTime;

        [Header("Overlap Prevention")]
        public float weight;
        public int weightLayer;

        [Header("Base Avatar Settings")]
        [SerializeField] protected ParticleEventBridge particleEventBridge = default;
        [SerializeField] protected PlayMakerFSM playMakerFSM = default;

        public Animator animator;
        public AvatarIdentifier avatarIdentifier;
        public Transform centerOfAvatar;

        [NonSerialized] public AvatarRecord avatarRecord;

        public List<Material> blotMaterials { get; private set; }

        private string lastAnimatorTrigger;

        private int CheckHealth(int value)
        {
            if (value < 0)
                return 0;

            if (value > maxHealth.value)
                return maxHealth.value;

            return value;
        }

        protected BaseAvatar()
        {
            health = new ControlledEventVariable<BaseAvatar, int>(this, 0, CheckHealth);
            maxHealth = new EventVariable<BaseAvatar, int>(this, 0);
            actionSpeed = new EventVariable<BaseAvatar, float>(this, 0);
            isInCombat = new EventVariable<BaseAvatar, bool>(this, false);
            isInvulnerable = new EventVariable<BaseAvatar, bool>(this, false);
            invulnerableDurationLeft = new EventVariable<BaseAvatar, float>(this, 0);
        }

        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void OnValueChanged_Health(int oldValue, int newValue)
        {
            int difference = newValue - oldValue;

            if (newValue == 0)
                Die();
            else if (difference < 0)
                TriggerOnTakeDamage(-difference);
        }

        
        private DamageResult TryApplyDamage(int damage)
        {
            health.value -= damage;
            return DamageResult.Success;
        }

        public virtual void Die()
        {
            if (isDead)
                return;

            TriggerOnDeath();
            isInCombat.value = false;
            isDead = true;
        }

        protected void TriggerOnTakeDamage(int damage)
        {
            if (onTakeDamage != null)
                onTakeDamage(this, damage);
        }

        protected void TriggerOnDeath()
        {
            if (onDeath != null)
                onDeath(this);
        }

        public void TriggerEvent(Enum eventType)
        {
            string eventName = eventType.GetIdentifier();
            playMakerFSM.SendEvent(eventName);
        }

        public void DestroyAvatar()
        {
            particleEventBridge.PreDestroy();
            GameObject.Destroy(gameObject);
        }

        public void SetAnimatorTrigger(string triggerName, bool playSameTrigger)
        {
            if (lastAnimatorTrigger == triggerName && !playSameTrigger)
                return;

            if (!string.IsNullOrEmpty(lastAnimatorTrigger))
                animator.ResetTrigger(lastAnimatorTrigger);

            lastAnimatorTrigger = triggerName;
            animator.SetTrigger(triggerName);
        }

        protected virtual void OnDestroy()
        {
            
        }
    }
}

