using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletJam.Player
{
    using Core.Input;

    public class AimAndFire : MonoBehaviour
    {
        [SerializeField, NotNull] private Transform weaponHolder;
        [SerializeField] private float offset;

        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            Vector2 mousePosInWorld = cam.ScreenToWorldPoint(InputManager.Instance.LookInputs);
            Vector2 dir = mousePosInWorld - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + offset;
            weaponHolder.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}