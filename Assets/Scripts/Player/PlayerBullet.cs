using System;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerBullet : NetworkBehaviour {

        [SerializeField] private float speed = 100f;
        
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
        }

        private void Update()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            MoveBulletServerRpc(transform.position);
        }

        private void OnTriggerExit(Collider other)
        {
            Destroy(gameObject);
        }

        [ServerRpc(RequireOwnership = false)]
        private void MoveBulletServerRpc(Vector3 position)
        {
            // MoveBulletClientRpc(position);       
            transform.position = position;
        }
    }
}
