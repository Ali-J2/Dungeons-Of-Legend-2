using System;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonBrickStudios
{
    public class SubTriggerCollider : MonoBehaviour
    {
        public event Action<Collider> onTriggerEnter;
        public event Action<Collider> onTriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (onTriggerEnter != null)
                onTriggerEnter(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (onTriggerExit != null)
                onTriggerExit(other);
        }
    }
}
