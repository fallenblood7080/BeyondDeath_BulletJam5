using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletJam
{
    public class TestAttack : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return StartCoroutine(AttackPattern.CirclePattern(transform.position, 7f, 20f, 16f));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(AttackPattern.CirclePattern(transform.position, 7f, 20f, 50, 0.5f, 0.1f, 20));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(AttackPattern.AstroidPattern(transform.position, 7f, 20f, 50, 0.5f, 0.0f));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(AttackPattern.AstroidPattern(transform.position, 7f, 20f, 50, 0.5f, 0.2f));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(AttackPattern.RosePattern(transform.position, 7f, 20f, 100, 0.5f, 0.0f));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(AttackPattern.RosePattern(transform.position, 7f, 20f, 100, 0.5f, 0.2f));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(AttackPattern.Hypotrochoid(transform.position, 7f, 20f, 100, 0.5f, 0.0f, 10));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(AttackPattern.Hypotrochoid(transform.position, 7f, 20f, 100, 0.5f, 0.2f));
        }
    }
}