using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnitySingleton;

namespace BulletJam.Pooler
{
    using Core;
    using Helper;

    public class PlayerBulletPooler : MonoSingleton<PlayerBulletPooler>
    {
        private PoolData poolData;
        private PlayerBullet bullets;

        public IObjectPool<PlayerBullet> BulletPool { get; private set; }

        private int size;
        private int maxSize;

        protected override void Awake()
        {
            base.Awake();
            int index = Array.FindIndex(GameManager.Instance.PoolSetting.datas, (data) => data.PoolName == "PlayerBullet");

            if (index == -1)
            {
                Logger.Instance.Log("PlayerBullet pool is not found in pool settings", LogGroup.Gameplay);
                return;
            }
            poolData = GameManager.Instance.PoolSetting.datas[index];

            bullets = poolData.PooledObject.GetComponent<PlayerBullet>();
            size = poolData.Size;
            maxSize = poolData.MaxSize;

            BulletPool = new ObjectPool<PlayerBullet>(CreateBulletPool, OnGetBullet, OnReleaseBullet, OnDestroyBullet, true, size, maxSize);
        }

        public void Get(float force, float dmg, Vector2 dir)
        {
            var bullet = BulletPool.Get();
            bullet.Init(force, dmg, dir, BulletPool);
        }

        private void OnDestroy()
        {
            BulletPool.Clear();
        }

        private PlayerBullet CreateBulletPool()
        {
            return Instantiate(bullets);
        }

        private void OnGetBullet(PlayerBullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private void OnReleaseBullet(PlayerBullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        private void OnDestroyBullet(PlayerBullet bullet)
        {
            Destroy(bullet.gameObject);
        }
    }
}