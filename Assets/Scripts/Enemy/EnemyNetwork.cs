using Player;
using Unity.Netcode;
using UnityEngine;

namespace Enemy
{
    public class EnemyNetwork : NetworkBehaviour
    {
        private GameObject[] _player;
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
        }
        private void Update()
        {
            _player = GameObject.FindGameObjectsWithTag("Player");
            if (_player.Length > 0)
            {
                var player = _player[0];
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime);
                
                MoveEnemyServerRpc();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer) return;
            if (other.GetComponent<PlayerBullet>())
            {
                GetComponent<NetworkHealthState>().health.Value -= 30;
            }
            else if (other.GetComponent<PlayerMovement>())
            {
                other.GetComponent<NetworkHealthState>().health.Value -= 10;
            }
        }
        [ServerRpc(RequireOwnership = false)]
        public void DestroyEnemyServerRpc()
        {
            Destroy(gameObject);
        }
        [ServerRpc(RequireOwnership = false)]
        private void MoveEnemyServerRpc()
        {
            transform.position += transform.forward * Time.deltaTime;
        }
    }
}