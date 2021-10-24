using System;
using UnityEngine;

namespace DungeonBrickStudios
{
    public class PlayerMovementITween : AvatarMovementBase
    {
        private const float ROTATION_RIGHT = 90;
        private const float ROTATION_LEFT = -90;

        public float turnTime;
        public iTween.EaseType movementEaseType;
        public iTween.EaseType turningEaseType;

        private bool rotating;
        LayerMask floorLayerMask;

        private void Start()
        {
            floorLayerMask = LayerMask.GetMask("Floor");
        }

        private void Update()
        {
            if (!isMoving.value)
                return;

            Vector3 adjustedPosition = new Vector3(transform.position.x, GetFloorYPositionUnderObject(transform.position), transform.position.z);
            transform.position = adjustedPosition;
        }

        public void HandleMovement(Vector3 moveDirection)
        {
            if (isMoving.value)
                return;

            isMoving.value = true;

            iTween.MoveBy(this.gameObject, iTween.Hash(
                "amount", moveDirection * stepDistance,
                "time", moveTime,
                "easeType", movementEaseType,
                "oncomplete", "ITweenCallback_OnMoveComplete"
                ));
        }

        public void HandleRotation(bool left)
        {
            if (left)
                RotatePlayer(ROTATION_LEFT);
            else
                RotatePlayer(ROTATION_RIGHT);
        }

        private void RotatePlayer(float rotationAmount)
        {
            if (rotating)
                return;

            iTween.RotateAdd(this.gameObject, iTween.Hash(
                "y", rotationAmount,
                "time", turnTime,
                "easeType", turningEaseType,
                "oncomplete", "ITweenCallback_OnRotateComplete"
                ));
        }

        // Helper Methods
        private void ITweenCallback_OnMoveComplete()
        {
            isMoving.value = false;
        }

        private void ITweenCallback_OnRotateComplete()
        {
            rotating = false;
        }

        private float GetFloorYPositionUnderObject(Vector3 currentPosition)
        {
            Vector3 raycastStartPosition = currentPosition + (Vector3.up * 50);
            bool hit = Physics.Raycast(raycastStartPosition, Vector3.down, out RaycastHit hitInfo, 100, floorLayerMask, QueryTriggerInteraction.Collide);

            if (!hit)
                return currentPosition.y;
            else
                return hitInfo.point.y;
        }
    }
}
