using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerBulletSpawner : NetworkBehaviour
    {
        [SerializeField] private PlayerBullet bulletPrefab;
        [SerializeField] private float speed = 700f;
        [SerializeField] private float cooldown = 0.5f;
        [SerializeField] private Transform spawnerPoint;

        private float _lastFired = float.MinValue;
        private bool _fired;

        private void Update() {
            if (!IsOwner) return;

            if (Input.GetMouseButton(0) && _lastFired + cooldown < Time.time) {
                _lastFired = Time.time;
                var dir = transform.forward;

                // Send off the request to be executed on all clients
                RequestFireServerRpc(dir);

                // Fire locally immediately
                ExecuteShoot(dir);
                StartCoroutine(ToggleLagIndicator());
            }
        }

        [ServerRpc]
        private void RequestFireServerRpc(Vector3 dir) {
            FireClientRpc(dir);
        }

        [ClientRpc]
        private void FireClientRpc(Vector3 dir) {
            if (!IsOwner) ExecuteShoot(dir);
        }

        private void ExecuteShoot(Vector3 dir) {
            var bullet = Instantiate(bulletPrefab, spawnerPoint.position, Quaternion.identity);
            bullet.Init(dir * 700);
        }

        private void OnGUI() {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (_fired) GUILayout.Label("FIRED LOCALLY");
        
            GUILayout.EndArea();
        }
        
        private IEnumerator ToggleLagIndicator() {
            _fired = true;
            yield return new WaitForSeconds(0.2f);
            _fired = false;
        }
    }
}