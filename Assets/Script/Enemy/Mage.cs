using System.Collections;

using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

namespace BulletJam.Enemy
{
    public class Mage : MonoBehaviour, IDamageable
    {
        public enum AttackType
        { Circle, Rose, Astroid, Hypotrochoid }

        public AttackType type;

        [SerializeField] private Transform attackPoint;
        [SerializeField] private float maxHealth;
        [SerializeField] private float attackInterval;
        [SerializeField] private float force;
        [SerializeField] private float dmg;
        [SerializeField] private Light2D lamp;
        [SerializeField] private SpriteRenderer[] bodyParts;

        private float nextTimeToAttack;
        private float health;
        private bool isAttacking;
        private bool isActive;

        private void Start()
        {
            health = maxHealth;
            isActive = false;
            GetComponent<Collider2D>().enabled = false;
            foreach (var part in bodyParts)
            {
                part.color = new Color(1, 1, 1, 0);
            }
            LeanTween.value(0, 1, 1f).setOnUpdate((float val) =>
            {
                foreach (var part in bodyParts)
                {
                    if (part != null)
                    {
                        part.color = new Color(1, 1, 1, val);
                    }
                }
            }).setOnComplete(() =>
            {
                GetComponent<Collider2D>().enabled = true;
                isActive = true;
            });
        }

        private void Update()
        {
            if (!isActive)
                return;

            if (nextTimeToAttack <= 0 && !isAttacking)
            {
                nextTimeToAttack = attackInterval;
                StartCoroutine(Attack());
            }
            else
            {
                if (!isAttacking)
                {
                    nextTimeToAttack -= Time.deltaTime;
                }
            }
        }

        private IEnumerator Attack()
        {
            lamp.intensity = 8f;
            isAttacking = true;
            switch (type)
            {
                case AttackType.Circle:
                    yield return StartCoroutine(AttackPattern.CirclePattern(attackPoint.position, force, dmg, (int)Random.Range(16, 32), 0.2f, Random.Range(0, 0.4f), Random.Range(16, 32)));
                    break;

                case AttackType.Rose:
                    yield return StartCoroutine(AttackPattern.RosePattern(attackPoint.position, force, dmg, (int)Random.Range(16, 32), 0.2f, Random.Range(0, 0.4f), Random.Range(16, 32)));
                    break;

                case AttackType.Astroid:
                    yield return StartCoroutine(AttackPattern.AstroidPattern(attackPoint.position, force, dmg, (int)Random.Range(16, 32), 0.2f, Random.Range(0, 0.4f), Random.Range(16, 32)));
                    break;

                case AttackType.Hypotrochoid:
                    yield return StartCoroutine(AttackPattern.Hypotrochoid(attackPoint.position, force, dmg, (int)Random.Range(16, 32), 0.2f, Random.Range(0, 0.4f), Random.Range(16, 32)));
                    break;
            }

            isAttacking = false;
            lamp.intensity = 1f;
        }

        public void Damage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}