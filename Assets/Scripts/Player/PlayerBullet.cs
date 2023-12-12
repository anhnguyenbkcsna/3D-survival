using System;
using UnityEngine;

namespace Player
{
    public class PlayerBullet : MonoBehaviour {

        [SerializeField] private float speed = 10f;
        private Vector3 _dir;

        public void Init(Vector3 dir) {
            _dir = dir;
            Invoke(nameof(DestroyBall), 3);
        }
        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Enemy")) {
                other.gameObject.GetComponent<Enemy.EnemyBehaviour>().TakeDamage(1);
            }
            DestroyBall();
        }

        private void Update()
        {
            transform.position += _dir.normalized * Time.deltaTime * speed;
        }

        private void DestroyBall() {
            Destroy(gameObject);
        }
    }
    
}
