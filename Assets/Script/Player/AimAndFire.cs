using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletJam.Player
{
    using BulletJam.Pooler;
    using Core.Input;
    using UnityEngine.Rendering.Universal;

    public class AimAndFire : MonoBehaviour
    {
        [SerializeField, NotNull] private Transform weaponHolder;
        [SerializeField, NotNull] private Transform[] firingPoint;
        [SerializeField, NotNull] private SpriteRenderer gunSpriteRenderer;
        [SerializeField, NotNull] private Light2D weaponHeatEffectLight;
        [SerializeField] private float offset;
        [SerializeField, BeginGroup("Weapon")] private float bulletForce;
        [SerializeField] private float bulletDamage;
        [SerializeField] private float accuracyError;
        [SerializeField] private float weaponMaxHeat;
        [SerializeField] private float weaponCoolStartTime;
        [SerializeField] private float weaponHeatBuildUpMultiplier;
        [SerializeField] private float weaponCoolMultiplier;
        [SerializeField, EndGroup] private float fireRate;

        private float currentWeaponCooldown;
        private Camera cam;

        private float nextTimeToFire;
        private float weaponCurrentHeat;

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
            if (Mathf.Abs(angle) >= 90f)
            {
                gunSpriteRenderer.flipY = true;
            }
            else
            {
                gunSpriteRenderer.flipY = false;
            }

            if (InputManager.Instance.IsFiring && Time.time >= nextTimeToFire)
            {
                currentWeaponCooldown = weaponCoolStartTime;
                nextTimeToFire = Time.time + 1 / fireRate;
                if (weaponCurrentHeat < weaponMaxHeat)
                {
                    Fire(dir);
                }
            }
            else if (currentWeaponCooldown <= 0)
            {
                weaponCurrentHeat -= Time.deltaTime * weaponCoolMultiplier;
                if (weaponCurrentHeat < 0f)
                {
                    weaponCurrentHeat = 0f;
                }
            }
            else
            {
                currentWeaponCooldown -= Time.deltaTime;
            }

            weaponHeatEffectLight.intensity = weaponCurrentHeat;

            if (weaponCurrentHeat > weaponMaxHeat)
            {
                weaponCurrentHeat = weaponMaxHeat;
            }
        }

        private void Fire(Vector2 dir)
        {
            PlayerBulletPooler.Instance.Get(bulletForce, bulletDamage, dir, firingPoint[Random.Range(minInclusive: 0, maxExclusive: firingPoint.Length)].position);
            weaponCurrentHeat += Time.deltaTime * weaponHeatBuildUpMultiplier;
        }
    }
}