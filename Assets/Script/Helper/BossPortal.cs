using BulletJam.Core;
using BulletJam.Enemy;
using UnityEngine;

namespace BulletJam.Helper
{
    public class BossPortal : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Update()
        {
            if (Time.frameCount % 10 == 0)
            {
                if (FindObjectsByType<Mage>(sortMode: FindObjectsSortMode.None).Length == 0)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    if (Vector2.Distance(target.position, transform.position) < 1f)
                    {
                        GameManager.Instance.Play(1);
                    }
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
}