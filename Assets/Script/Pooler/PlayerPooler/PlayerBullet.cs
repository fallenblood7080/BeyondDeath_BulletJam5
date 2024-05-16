using UnityEngine;
using UnityEngine.Pool;

namespace BulletJam.Pooler
{
    public class PlayerBullet : MonoBehaviour
    {
        private Rigidbody2D rb;
        private IObjectPool<PlayerBullet> bulletPool;
        private SpriteRenderer spriteRenderer;
        private Collider2D col;
        private float damage;

        private ParticleSystem particle;

        private void Awake()
        {
            col = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            particle = GetComponentInChildren<ParticleSystem>();
        }

        public void Init(float force, float dmg, Vector2 dir, IObjectPool<PlayerBullet> pool)
        {
            spriteRenderer.enabled = true;
            col.enabled = true;

            if (rb == null)
                rb = GetComponent<Rigidbody2D>();

            damage = dmg;
            bulletPool = pool;
            rb.AddForce(dir * force, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Boss"))
            {
                if (collision.collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.Damage(10f);
                }
            }
            spriteRenderer.enabled = false;
            col.enabled = false;
            if (!particle.isPlaying)
            {
                particle.Play();
            }
            LeanTween.delayedCall(1f, () => bulletPool.Release(this));
        }
    }
}