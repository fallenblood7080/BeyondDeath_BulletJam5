using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletJam
{
    public class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}