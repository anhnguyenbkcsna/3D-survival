using System;
using UnityEngine;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public GameObject playerBulletPrefab;
        
        private int _hp = 3;
        private float _atkCd = 0.5f;

        private float _atkCdTimer = 0f;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            _atkCdTimer = _atkCd;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetTrigger("Attack");
                _atkCdTimer = _atkCd;
                PlayerAttack();
            }
        }

        private void FixedUpdate()
        {
            _atkCdTimer -= Time.fixedDeltaTime;
        }

        private void PlayerAttack()
        {
            Instantiate(playerBulletPrefab, transform.position, transform.rotation);
        }
        public void TakeDamage(int damage)
        {
            _hp -= damage;
            if (_hp <= 0)
            {
                _animator.SetBool("isDead", true);
            }
        }
    }
}