// Uses the new Unity Input System. These are the events that system will call on the gameobject it is attached to

using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace DungeonBrickStudios
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(AvatarMovement))]
    public class PlayerController : MonoBehaviour
    {
        private AvatarMovement playerMovement;

        private bool forwardPressed;
        private bool backwardPressed;
        private bool leftPressed;
        private bool rightPressed;
        private bool turnLeftPressed;
        private bool turnRightPressed;

        private void Awake()
        {
            playerMovement = this.GetComponent<AvatarMovement>();
        }

        private void FixedUpdate()
        {
            CheckMove();
            CheckRotate();
        }

        // Player Actions
        private void CheckMove()
        {
            if (forwardPressed)
                playerMovement.HandleMovement(transform.forward);
            else if (backwardPressed)
                playerMovement.HandleMovement(-transform.forward);
            else if (rightPressed)
                playerMovement.HandleMovement(transform.right);
            else if (leftPressed)
                playerMovement.HandleMovement(-transform.right);
        }

        private void CheckRotate()
        {
            if (turnLeftPressed)
                playerMovement.HandleRotation(true);
            else if (turnRightPressed)
                playerMovement.HandleRotation(false);
        }

        public void MoveForward(CallbackContext context)
        {
            if (context.started)
                forwardPressed = true;

            if (context.canceled)
                forwardPressed = false;
        }

        public void MoveBackward(CallbackContext context)
        {
            if (context.started)
                backwardPressed = true;

            if (context.canceled)
                backwardPressed = false;
        }

        public void StrafeLeft(CallbackContext context)
        {
            if (context.started)
                leftPressed = true;

            if (context.canceled)
                leftPressed = false;
        }

        public void StrafeRight(CallbackContext context)
        {
            if (context.started)
                rightPressed = true;

            if (context.canceled)
                rightPressed = false;
        }

        public void TurnLeft(CallbackContext context)
        {
            if (context.started)
                turnLeftPressed = true;

            if (context.canceled)
                turnLeftPressed = false;
        }

        public void TurnRight(CallbackContext context)
        {
            if (context.started)
                turnRightPressed = true;

            if (context.canceled)
                turnRightPressed = false;
        }

        public void EnableMouseLook(CallbackContext context)
        {

        }

        public void Attack(CallbackContext context)
        {

        }

        public void CastSpell(CallbackContext context)
        {

        }

        public void OpenMap(CallbackContext context)
        {

        }

        public void OpenSpells(CallbackContext context)
        {

        }

        public void OpenMenu(CallbackContext context)
        {

        }

        public void Slot0(CallbackContext context)
        {

        }

        public void Slot1(CallbackContext context)
        {

        }

        public void Slot2(CallbackContext context)
        {

        }

        public void Slot3(CallbackContext context)
        {

        }

        public void Slot4(CallbackContext context)
        {

        }

        public void Slot5(CallbackContext context)
        {

        }

        public void Slot6(CallbackContext context)
        {

        }

        public void Slot7(CallbackContext context)
        {

        }

        public void Slot8(CallbackContext context)
        {

        }

        public void Slot9(CallbackContext context)
        {

        }
    }
}
