using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletJam
{
    public class BoundaryGizmos : MonoBehaviour
    {
        [SerializeField] private float size;

        private void Start()
        {
        }

        private void Update()
        {
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(size, size, size));
        }
    }
}