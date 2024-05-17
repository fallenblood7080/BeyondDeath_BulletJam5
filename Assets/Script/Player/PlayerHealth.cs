using BulletJam.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BulletJam
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private Slider healthbar;
        [SerializeField] private GameObject gameOver;
        private float health;

        private void Start()
        {
            health = maxHealth;
            if (healthbar != null)
            {
                healthbar.value = health;
                healthbar.maxValue = maxHealth;
            }
        }

        public void Damage(float damage)
        {
            health -= damage;
            if (healthbar != null)
            {
                healthbar.value = health;
            }
            if (health <= 0)
            {
                gameOver.SetActive(true);
                EnemyManager enemy = FindAnyObjectByType<EnemyManager>();
                if (enemy != null)
                {
                    enemy.deadPlayerEnemy.AddDeathBody(transform.position);
                }
                Destroy(gameObject);
            }
        }
    }
}