using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletJam.Enemy
{
    [CreateAssetMenu(fileName = "PlayerDeadEnemyData", menuName = "Scriptable/PlayerDeadData")]
    public class PlayerDeadEnemyData : ScriptableObject
    {
        [AssetPreview] public GameObject playerDead;
        public List<Vector2> deadPlayerPosition = new List<Vector2>();

        public void AddDeathBody(Vector2 pos)
        {
            deadPlayerPosition.Add(pos);
        }

        public void RemoveDeathBody(Vector2 pos)
        {
            deadPlayerPosition.Remove(pos);
        }
    }
}