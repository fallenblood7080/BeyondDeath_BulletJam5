using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BulletJam
{
    public class LoadingTips : MonoBehaviour
    {
        [Multiline, SerializeField] private string[] tips;

        [SerializeField] private TMP_Text loadingText;

        private void OnEnable()
        {
            loadingText.text = tips[(int)Random.Range(minInclusive: 0, maxInclusive: tips.Length - 1)];
        }
    }
}