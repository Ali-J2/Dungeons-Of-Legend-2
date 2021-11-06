using System;
using UnityEngine;

namespace DungeonBrickStudios
{
    public class PlayerMovement : AvatarMovementBase
    {
        private const float ROTATION_DEGREES = 90f;

        public float turnTime;

        private bool rotating;
        LayerMask floorLayerMask;
        
        private Vector3 targetPosition;
        private Vector3 targetRotation;
        private float moveSpeed;
        private float turnSpeed;

        protected override void Start()
        {
            base.Start();
            floorLayerMask = LayerMask.GetMask("Floor");

            moveSpeed = stepDistance / moveTime;
            turnSpeed = ROTATION_DEGREES / turnTime;

            Vector3 adjustedPosition = new Vector3(transform.position.x, GetFloorYPosition(transform.position), transform.position.z);
            transform.position = adjustedPosition;
            targetRotation = transform.rotation.eulerAngles;
        }

        private void Update()
        {
            MovePlayer();
            RotatePlayer();
        }

        public void HandleMovement(Vector3 moveDirection)
        {
            if (isMoving.value)
                return;

            if (rotating)
                return;

            targetPosition = transform.position + (moveDirection * stepDistance);

            if (!TargetPositionValid())
                return;

            isMoving.value = true;

            GridManager.instance.SetGridPositionWalkable(transform.position, true);
            GridManager.instance.SetGridPositionWalkable(targetPosition, false);
        }

        public void HandleRotation(bool left)
        {
            if (rotating)
                return;

            if (left)
                targetRotation -= Vector3.up * ROTATION_DEGREES;
            else
                targetRotation += Vector3.up * ROTATION_DEGREES;

            if (targetRotation.y > 270f && targetRotation.y < 361f)
                targetRotation.y = 0f;

            if (targetRotation.y < 0f)
                targetRotation.y = 270f;

            rotating = true;
        }

        private void MovePlayer()
        {
            if (!isMoving.value)
                return;

            targetPosition.y = GetFloorYPosition(targetPosition);

            Vector3 destination = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            destination.y = GetFloorYPosition(destination);
            
            transform.position = destination;

            if (transform.position == targetPosition)
                isMoving.value = false;
        }
        private void RotatePlayer()
        {
            if (!rotating)
                return;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * turnSpeed);

            if (transform.rotation.eulerAngles == targetRotation)
                rotating = false;
        }
        private float GetFloorYPosition(Vector3 pos)
        {
            Vector3 raycastStartPosition = pos + (Vector3.up * 150);
            bool hit = Physics.Raycast(raycastStartPosition, Vector3.down, out RaycastHit hitInfo, 300, floorLayerMask, QueryTriggerInteraction.Collide);

            if (!hit)
                return pos.y;
            else
                return hitInfo.point.y;
        }

        private bool TargetPositionValid()
        {
            if (!GridManager.instance.PositionIsWalkable(targetPosition))
                return false;

            return true;
        }
    }
}
