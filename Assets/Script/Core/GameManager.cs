using System.Collections;
using UnityEngine;
using UnitySingleton;

namespace BulletJam.Core
{
    using Helper;

    public class GameManager : PersistentMonoSingleton<GameManager>
    {
        [field: SerializeField, Disable] public SceneContainerScriptable SceneContainer { get; private set; }
        [field: SerializeField, Disable] public Pooler.PoolSettings PoolSetting { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(DoGameInit());
        }

        private IEnumerator DoGameInit()
        {
#if UNITY_EDITOR
            Logger.CreateInstance();
            yield return StartCoroutine(Logger.Instance.LoadLogSettings());
#endif

            LoadingManager.CreateInstance();
            yield return StartCoroutine(LoadingManager.Instance.GetLoadingScreenObject());

            yield return StartCoroutine(HelperCoroutine.LoadDataFromResources("Scriptable/SceneContainer",
                (data) => SceneContainer = data as SceneContainerScriptable));
            yield return StartCoroutine(HelperCoroutine.LoadDataFromResources("Scriptable/PoolSettings",
                (data) => PoolSetting = data as Pooler.PoolSettings));
        }
    }
}