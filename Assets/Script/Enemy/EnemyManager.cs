using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BulletJam.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private EnemySpawnData[] spawnDatas;
        [SerializeField] private GameObject[] emenies;

        public PlayerDeadEnemyData deadPlayerEnemy;

        private void Start()
        {
            foreach (var pos in deadPlayerEnemy.deadPlayerPosition)
            {
                Instantiate(deadPlayerEnemy.playerDead, pos, Quaternion.identity);
            }
        }

        private void Update()
        {
            if (player == null)
                return;

            for (int j = 0; j < spawnDatas.Length; j++)
            {
                EnemySpawnData spawnData = spawnDatas[j];
                if (player.position.y > spawnData.spawn.position.y && !spawnData.isSpawned)
                {
                    foreach (var item in spawnData.points)
                    {
                        int i = Random.Range(minInclusive: 0, maxExclusive: emenies.Length);
                        Instantiate(emenies[i], item.position, Quaternion.identity);
                    }

                    spawnData.isSpawned = true;
                    break;
                }
            }
        }
    }

    [Serializable]
    public class EnemySpawnData
    {
        public Transform spawn;
        public bool isSpawned;
        public Transform[] points;
    }
}