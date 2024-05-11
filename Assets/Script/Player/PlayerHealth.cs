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

        private float health;

        private void Start()
        {
            health = maxHealth;
            healthbar.value = health;
            healthbar.maxValue = maxHealth;
        }

        public void Damage(float damage)
        {
            health -= damage;
            healthbar.value = health;
        }
    }
}