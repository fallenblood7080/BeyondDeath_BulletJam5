using BulletJam.Enemy;
using BulletJam.Helper;
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
        [SerializeField] private AudioSource source;

        private void Start()
        {
            source = GetComponent<AudioSource>();
            health = maxHealth;
            if (healthbar != null)
            {
                healthbar.value = health;
                healthbar.maxValue = maxHealth;
            }
        }

        public void Damage(float damage)
        {
            CameraShake.instance.Shake(5, 0.5f);
            if (!source.isPlaying)
            {
                source.Play();
            }
            health -= damage;
            if (healthbar != null)
            {
                healthbar.value = health;
            }
            if (health <= 0)
            {
		Cursor.visible = true;
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