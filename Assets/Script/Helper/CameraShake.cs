using Cinemachine;
using System.Linq;
using UnityEngine;

namespace BulletJam.Helper
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake instance;

        private CinemachineVirtualCamera virtualCamera;
        private float shakeTime;

        private float initialAmplitude, initialFrequency;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            initialAmplitude = noise.m_AmplitudeGain;
            initialFrequency = noise.m_FrequencyGain;
        }

        // Update is called once per frame
        private void Update()
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    noise.m_AmplitudeGain = initialAmplitude;
                    noise.m_FrequencyGain = initialFrequency;
                }
            }
        }

        public void Shake(float intensity, float timer, float freq = 1)
        {
            CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noise.m_AmplitudeGain = intensity;
            noise.m_FrequencyGain = freq;
            shakeTime = timer;
        }
    }
}