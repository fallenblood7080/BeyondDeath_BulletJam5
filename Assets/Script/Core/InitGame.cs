using BulletJam.Core;
using System.Collections;
using System.Collections.Generic;
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