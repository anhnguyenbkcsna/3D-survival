﻿using Cinemachine;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        public CharacterController controller;
        [SerializeField] private CinemachineFreeLook freeLookCamera;
        
        public float speed = 6f;
        public float turnSmoothTime = 0.1f;
        public float turnSmoothVelocity;
        
        private Transform _mainCamera;

        private void Start()
        {
            _mainCamera = GameObject.FindWithTag("MainCamera").transform;
        }
        public override void OnNetworkSpawn()
        {
            if (!IsLocalPlayer)
            {
                enabled = false;
                freeLookCamera.Priority = 0;
            }
            else
            {
                freeLookCamera.Priority = 10;
                freeLookCamera.Follow = transform;
            }
        }

        private void Update()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            var direction = new Vector3(horizontal, 0f, vertical).normalized;
            
            if(direction.magnitude >= 0.1f)
            {
                // Rotate the player to face the direction of movement.
                var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                
                var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * (speed * Time.deltaTime));
            }
        }
    }
}