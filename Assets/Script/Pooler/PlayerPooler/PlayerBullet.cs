using UnityEngine;
using UnityEngine.Pool;

namespace BulletJam.Pooler
{
    public class PlayerBullet : MonoBehaviour
    {
        private Rigidbody2D rb;
        private IObjectPool<PlayerBullet> bulletPool;

        private float damage;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Init(float force, float dmg, Vector2 dir, IObjectPool<PlayerBullet> pool)
        {
            if (rb == null)
                rb = GetComponent<Rigidbody2D>();

            damage = dmg;
            bulletPool = pool;
            rb.AddForce(dir * force, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.GetComponent<IDamageable>() != null)
            {
                bulletPool.Release(this);
            }
        }
    }
}