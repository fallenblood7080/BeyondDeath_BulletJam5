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
            StartCoroutine(HelperCoroutine.LoadDataFromResources("Scriptable/LoggerSettings", (data) => settings = data as LoggerSettings));
        }
    }
}