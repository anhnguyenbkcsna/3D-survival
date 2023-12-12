using System;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerBullet : NetworkBehaviour {

        [SerializeField] private float speed = 1f;
        
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
        }

        private void Update()
        {
            transform.position += transform.forward * speed;
            MoveBulletServerRpc(transform.position);
        }
        [ClientRpc]
        private void MoveBulletClientRpc(Vector3 position)
        {
            transform.position = position;
        }
        [ServerRpc(RequireOwnership = false)]
        private void MoveBulletServerRpc(Vector3 position)
        {
            // MoveBulletClientRpc(position);       
            transform.position = position;
        }
    }
}
