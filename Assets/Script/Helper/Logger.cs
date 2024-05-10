using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySingleton;

namespace BulletJam.Helper
{
    public class Logger : PersistentMonoSingleton<Logger>
    {
        private LoggerSettings settings;

        protected override void Awake()
        {
            base.Awake();
#if UNITY_EDITOR
            StartCoroutine(HelperCoroutine.LoadDataFromResources("Scriptable/LoggerSettings", (data) => settings = data as LoggerSettings));
#endif
        }

        public void Log(object msg, LogGroup group = LogGroup.None, UnityEngine.Object caller = null)
        {
#if UNITY_EDITOR

            if (settings == null)
                Debug.LogError($"{msg}\nLogger is not Initialize.");

            LogData data = Array.Find(settings.Datas, (data) => data.group == group);
            Debug.Log($"<color=#{data.color}><size={data.fontSize}>{msg}</size></color>", caller);
#endif
        }
    }
}