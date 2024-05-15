using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BulletJam.Enemy
{
    public class Boss : MonoBehaviour
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
                //bossCollider.enabled = true;
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
            int attackRandomIndex = (int)Random.Range(minInclusive: 0, maxInclusive: 5);
            switch (attackRandomIndex)
            {
                case 0:
                    yield return StartCoroutine(SwitchPlace());
                    break;

                case 1:
                    yield return StartCoroutine(AttackPattern.CirclePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 16, 1, offset: Random.Range(0, 16)));
                    break;

                case 2:
                    yield return StartCoroutine(AttackPattern.CirclePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 8, 1f, 0.2f, Random.Range(0, 8)));
                    break;

                case 3:
                    yield return StartCoroutine(AttackPattern.RosePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 16, 1f, offset: Random.Range(0, 16)));
                    break;

                case 4:
                    yield return StartCoroutine(AttackPattern.RosePattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 32, 1f, 0.1f, offset: Random.Range(0, 32)));
                    break;

                case 5:
                    yield return StartCoroutine(AttackPattern.AstroidPattern(attackPoint.localToWorldMatrix.GetPosition(), force, dmg, 32, 1f, offset: Random.Range(0, 32)));
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

            currentBossPointIndex = randomIndex;
            bossCollider.enabled = false;
            gfx.SetActive(false);
            yield return new WaitForSeconds(1f);
            transform.position = bossPoints[currentBossPointIndex].position;
            bossCollider.enabled = true;
            gfx.SetActive(true);
        }
    }
}