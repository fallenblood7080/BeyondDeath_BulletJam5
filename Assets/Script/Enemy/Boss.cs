using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BulletJam.Enemy
{
    public class Boss : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject bossCam;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float maxHealth = 1000;
        [SerializeField] private Slider healthBar;
        [SerializeField] private float force, dmg;

        [Space(5f)]
        [SerializeField] private float minAttackInterval;

        [SerializeField] private float maxAttackInterval;

        [Space(5f)]
        [SerializeField] private Transform attackPoint;

        [SerializeField] private Transform[] bossPoints;
        [SerializeField] private SpriteRenderer[] bodyParts;
        [SerializeField] private ParticleSystem particle;

        [Space(5f)]
        [SerializeField] private GameObject gfx;

        private int currentBossPointIndex;

        private float currentAttackInterval;

        private Collider2D bossCollider;

        private float currentHealth;
        private bool isBossActive;
        private bool isAttacking;

        private IEnumerator Start()
        {
            isBossActive = false;
            bossCollider = GetComponent<Collider2D>();
            bossCollider.enabled = false;
            currentHealth = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
            yield return new WaitForSeconds(0.5f);
            bossCam.SetActive(false);
            ShowBossCanvas();
        }

        private void ShowBossCanvas()
        {
            LeanTween.alphaCanvas(canvasGroup, 1, 0.5f).setEaseInCirc().setOnComplete(() =>
            {
                bossCollider.enabled = true;
                isBossActive = true;
            });
        }

        private void Update()
        {
            if (!isBossActive)
                return;
            if (currentAttackInterval <= 0 && !isAttacking)
            {
                currentAttackInterval = Random.Range(minAttackInterval, maxAttackInterval);
                StartCoroutine(Attack());
            }
            else
            {
                if (!isAttacking)
                {
                    currentAttackInterval -= Time.deltaTime;
                }
            }
        }

        private IEnumerator Attack()
        {
            isAttacking = true;
            int attackRandomIndex = (int)Random.Range(minInclusive: 0, maxInclusive: 7);
            switch (attackRandomIndex)
            {
                case 0:
                    yield return StartCoroutine(SwitchPlace());
                    break;

                case 1:
                    yield return StartCoroutine(AttackPattern.CirclePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 32, 1, offset: Random.Range(0, 16)));
                    break;

                case 2:
                    yield return StartCoroutine(AttackPattern.CirclePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 64, 1f, 0.2f, Random.Range(0, 64)));
                    break;

                case 3:
                    yield return StartCoroutine(AttackPattern.RosePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 32, 1f, offset: Random.Range(0, 16)));
                    break;

                case 4:
                    yield return StartCoroutine(AttackPattern.RosePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 64, 1f, 0.1f, offset: Random.Range(0, 32)));
                    break;

                case 5:
                    yield return StartCoroutine(AttackPattern.AstroidPattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 64, 1f, offset: Random.Range(0, 32)));
                    break;

                case 6:
                    yield return StartCoroutine(AttackPattern.CirclePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 64, 1f, 0f, Random.Range(0, 64)));
                    yield return new WaitForSeconds(0.5f);
                    yield return StartCoroutine(AttackPattern.CirclePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 32, 1, offset: Random.Range(0, 32)));
                    break;

                case 7:
                    yield return StartCoroutine(AttackPattern.RosePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 16, 1f, offset: Random.Range(0, 32)));
                    yield return new WaitForSeconds(0.5f);
                    yield return StartCoroutine(AttackPattern.RosePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 32, 1f, offset: Random.Range(0, 16)));
                    break;
            }
            isAttacking = false;
        }

        private IEnumerator SwitchPlace()
        {
            int randomIndex = Random.Range(minInclusive: 0, maxExclusive: bossPoints.Length);
            while (randomIndex == currentBossPointIndex)
            {
                randomIndex = Random.Range(minInclusive: 0, maxExclusive: bossPoints.Length);
            }
            if (!particle.isPlaying)
            {
                particle.Play();
            }
            yield return new WaitForSeconds(1);
            currentBossPointIndex = randomIndex;
            bossCollider.enabled = false;
            HideBody();
            yield return new WaitForSeconds(1f);
            transform.position = bossPoints[currentBossPointIndex].position;
            if (!particle.isPlaying)
            {
                particle.Play();
            }
            yield return new WaitForSeconds(1f);
            bossCollider.enabled = true;
            ShowBody();
        }

        private void HideBody()
        {
            foreach (var part in bodyParts)
            {
                LeanTween.value(1, 0, 0.4f).setOnUpdate((float val) =>
                {
                    part.color = new Color(1, 1, 1, val);
                });
            }
        }

        private void ShowBody()
        {
            foreach (var part in bodyParts)
            {
                LeanTween.value(0, 1, 0.4f).setOnUpdate((float val) =>
                {
                    part.color = new Color(1, 1, 1, val);
                });
            }
        }

        public void Damage(float damage)
        {
            currentHealth -= damage;
            healthBar.value = currentHealth;
        }
    }
}