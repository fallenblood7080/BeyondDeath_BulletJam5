using BulletJam.Core;
using BulletJam.Helper;
using System;
using UnityEngine;
using UnityEngine.Pool;
using UnitySingleton;
using Logger = BulletJam.Helper.Logger;

namespace BulletJam.Pooler
{
    public class EnemyBulletPooler : MonoSingleton<EnemyBulletPooler>
    {
        private PoolData poolData;
        private EnemyBullet bullets;

        public IObjectPool<EnemyBullet> BulletPool { get; private set; }

        private int size;
        private int maxSize;

        protected override void Awake()
        {
            base.Awake();
            int index = Array.FindIndex(GameManager.Instance.PoolSetting.datas, (data) => data.PoolName == "EnemyBullet");

            if (index == -1)
            {
                Logger.Instance.Log("PlayerBullet pool is not found in pool settings", LogGroup.Gameplay);
                return;
            }
            poolData = GameManager.Instance.PoolSetting.datas[index];

            bullets = poolData.PooledObject.GetComponent<EnemyBullet>();
            size = poolData.Size;
            maxSize = poolData.MaxSize;

            BulletPool = new ObjectPool<EnemyBullet>(CreateBulletPool, OnGetBullet, OnReleaseBullet, OnDestroyBullet, true, size, maxSize);
        }

        public void Get(float force, float dmg, Vector2 dir, Vector2 pos)
        {
            var bullet = BulletPool.Get();
            bullet.transform.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg)));
            bullet.Init(force, dmg, dir, BulletPool);
        }

        private EnemyBullet CreateBulletPool()
        {
            return Instantiate(bullets);
        }

        private void OnGetBullet(EnemyBullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private void OnReleaseBullet(EnemyBullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        private void OnDestroyBullet(EnemyBullet bullet)
        {
            if (bullet != null)
            {
                Destroy(bullet.gameObject);
            }
        }
    }
}