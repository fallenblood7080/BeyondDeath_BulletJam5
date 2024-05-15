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

        [Space(5f)]
        [SerializeField] private float minAttackInterval;

        [SerializeField] private float maxAttackInterval;

        [Space(5f)]
        [SerializeField] private Transform[] bossPoints;

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
            yield return StartCoroutine(SwitchPlace());
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
            transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            transform.position = bossPoints[currentBossPointIndex].position;
            bossCollider.enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}