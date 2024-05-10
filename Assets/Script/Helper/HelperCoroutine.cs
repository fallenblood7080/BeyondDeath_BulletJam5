using System;
using System.Collections;
using UnityEngine;

namespace BulletJam.Helper
{
    public class HelperCoroutine
    {
        public static IEnumerator LoadDataFromResources(string path, Action<UnityEngine.Object> callback)
        {
            ResourceRequest request = Resources.LoadAsync(path);
            while (!request.isDone)
            {
                yield return null;
            }
            if (request.asset != null)
            {
                callback?.Invoke(request.asset);
            }
        }

        public static IEnumerator Countdown(float initialtime, Action<float> onTimerUpdate = null, Action onComplete = null)
        {
            float timer = initialtime;
            while (timer >= 0)
            {
                timer -= Time.deltaTime;
                onTimerUpdate?.Invoke(timer);
                yield return null;
            }
            onComplete?.Invoke();
        }
    }
}