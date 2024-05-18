using BulletJam.Core;
using UnityEngine;

namespace BulletJam
{
    public class InitGame : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.CreateInstance();
        }
    }
}