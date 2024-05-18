using BulletJam.Player;
using BulletJam.Pooler;
using UnityEngine;

namespace BulletJam.Enemy
{
    public class PlayerZombiew : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform target;
        [SerializeField] private float detectionRadius;
        [SerializeField] private Transform holder;
        [SerializeField] private Transform[] firingPoint;

        [SerializeField] private float bulletForce, bulletDamage;
        [SerializeField] private float weaponFireRate;
        [SerializeField] private float attackCoolDown;
        [SerializeField] private float rotationSpeed;

        [Space(5f)]
        [SerializeField] private float maxHealth;

        private float currentHealth;

        private float nextTimeToAttack;
        private float attakCurrentCooldowm;
        private float nextTimeToFire;
        private bool isReady;

        private void Start()
        {
            target = FindAnyObjectByType<AimAndFire>().transform;
            currentHealth = maxHealth;
        }

        private void Update()
        {
            if (target == null)
                return;

            if (Vector2.Distance(target.position, transform.position) < detectionRadius)
            {
                Vector2 dir = target.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                holder.rotation = Quaternion.RotateTowards(holder.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);

                if (isReady)
                {
                    if (Time.time >= nextTimeToFire)
                    {
                        nextTimeToFire = Time.time + 1 / weaponFireRate;
                        Fire(firingPoint[0].right);
                        nextTimeToAttack += Time.deltaTime;
                    }
                }

                if (nextTimeToAttack > attackCoolDown)
                {
                    attakCurrentCooldowm += Time.deltaTime;
                    if (attakCurrentCooldowm > attackCoolDown)
                    {
                        isReady = true;
                        nextTimeToAttack = 0;
                        attakCurrentCooldowm = 0;
                    }
                    else
                    {
                        isReady = false;
                    }
                }
                else
                {
                    isReady = true;
                    attakCurrentCooldowm = 0;
                }
            }
        }

        private void Fire(Vector2 dir)
        {
            EnemyBulletPooler.Instance.Get(bulletForce, bulletDamage, dir.normalized, firingPoint[UnityEngine.Random.Range(minInclusive: 0, maxExclusive: firingPoint.Length)].position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }

        public void Damage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                FindAnyObjectByType<EnemyManager>().deadPlayerEnemy.RemoveDeathBody(transform.position);
                Destroy(gameObject);
            }
        }
    }
}