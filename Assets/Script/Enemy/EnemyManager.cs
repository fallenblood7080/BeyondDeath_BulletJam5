using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletJam.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private EnemySpawnData[] spawnDatas;

        private void Update()
        {
            if (player == null)
                return;

            foreach (var spawnData in spawnDatas)
            {
                if (player.position.y > spawnData.spawn.position.y && !spawnData.isSpawned)
                {
                    break;
                }
            }
        }
    }

    [Serializable]
    public struct EnemySpawnData
    {
        public Transform spawn;
        public bool isSpawned;
        public Transform[] points;
    }
}