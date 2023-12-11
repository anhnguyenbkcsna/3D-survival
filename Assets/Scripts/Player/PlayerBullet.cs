using UnityEngine;

namespace Player
{
    public class PlayerBullet : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        private Rigidbody _rb;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _rb.velocity = transform.forward * speed;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy.EnemyBehaviour>().TakeDamage(1);
                Debug.Log("Bullet hit enemy");
                Destroy(gameObject);
            }
        }
    }
}