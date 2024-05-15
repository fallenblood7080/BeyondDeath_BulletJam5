using BulletJam.Pooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletJam
{
    public static class AttackPattern
    {
        public static IEnumerator CirclePattern(Vector2 center, float force, float dmg, float bulletCount, float radius = 0.5f, float eachBulletDelay = 0f, int offset = 0)
        {
            float angle = 2 * Mathf.PI / bulletCount;
            for (int i = 0 + offset; i < bulletCount + offset; i++)
            {
                float x = Mathf.Sin(angle * i) * radius;
                float y = Mathf.Cos(angle * i) * radius;
                yield return new WaitForSeconds(eachBulletDelay);
                EnemyBulletPooler.Instance.Get(force, dmg, (new Vector2(x + center.x, y + center.y) - center).normalized, new Vector2(x + center.x, y + center.y));
            }
        }

        public static IEnumerator ArcPattern(Vector2 center, float startAngle, float endAngle, float force, float dmg, float bulletCount, float radius = 0.5f, float eachBulletDelay = 0f)
        {
            float angleIncrement = (endAngle - startAngle) / bulletCount;
            float angle = startAngle;
            for (int i = 0; i < bulletCount; i++)
            {
                float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
                float y = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                yield return new WaitForSeconds(eachBulletDelay);
                EnemyBulletPooler.Instance.Get(force, dmg, (new Vector2(x + center.x, y + center.y) - center).normalized, new Vector2(x + center.x, y + center.y));
                angle += angleIncrement;
            }
        }

        public static IEnumerator AstroidPattern(Vector2 center, float force, float dmg, float count, float radius = 0.5f, float eachBulletDelay = 0f, int offset = 0)
        {
            float angle = 2 * Mathf.PI / count;
            for (int i = 0 + offset; i < count + offset; i++)
            {
                float x = Mathf.Pow(Mathf.Sin(angle * i), 3) * radius;
                float y = Mathf.Pow(Mathf.Cos(angle * i), 3) * radius;

                yield return new WaitForSeconds(eachBulletDelay);
                EnemyBulletPooler.Instance.Get(force, dmg, (new Vector2(x + center.x, y + center.y) - center).normalized, new Vector2(x + center.x, y + center.y));
            }
        }

        public static IEnumerator RosePattern(Vector2 center, float force, float dmg, float count, float radius = 0.5f, float eachBulletDelay = 0f, int offset = 0)
        {
            float angle = 2 * Mathf.PI / count;
            for (int i = 0 + offset; i < count + offset; i++)
            {
                float x = Mathf.Sin(angle * i) * Mathf.Sin(angle * i * 4) * radius;
                float y = Mathf.Cos(angle * i) * Mathf.Sin(angle * i * 4) * radius;

                yield return new WaitForSeconds(eachBulletDelay);
                EnemyBulletPooler.Instance.Get(force, dmg, (new Vector2(x + center.x, y + center.y) - center).normalized, new Vector2(x + center.x, y + center.y));
            }
        }

        public static IEnumerator Hypotrochoid(Vector2 center, float force, float dmg, float count, float radius = 0.5f, float eachBulletDelay = 0f, int offset = 0)
        {
            float angle = 2 * Mathf.PI / count;
            for (int i = 0 + offset; i < count + offset; i++)
            {
                float x = (2 * Mathf.Sin(angle * i) - 5 * Mathf.Sin(2 * (angle * i) / 3)) * radius;
                float y = (2 * Mathf.Cos(angle * i) - 5 * Mathf.Cos(2 * (angle * i) / 3)) * radius;

                yield return new WaitForSeconds(eachBulletDelay);
                EnemyBulletPooler.Instance.Get(force, dmg, (new Vector2(x + center.x, y + center.y) - center).normalized, new Vector2(x + center.x, y + center.y));
            }
        }
    }
}