using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonBrickStudios
{
    public class AvatarMovementBase : MonoBehaviour
    {
        public readonly EventVariable<AvatarMovementBase, bool> isMoving;
        public float moveTime;
        public float stepDistance;

        protected AvatarMovementBase()
        {
            isMoving = new EventVariable<AvatarMovementBase, bool>(this, false);
        }
    }
}
