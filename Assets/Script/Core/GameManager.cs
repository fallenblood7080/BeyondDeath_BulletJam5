using System.Collections;
using UnityEngine;
using UnitySingleton;

namespace BulletJam.Core
{
    using BulletJam.Pooler;
    using Helper;
    using UnityEngine.SceneManagement;

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
            LoadingManager.Instance.ShowLoadingScreen();

            yield return StartCoroutine(HelperCoroutine.LoadDataFromResources("Scriptable/SceneContainer",
                (data) => SceneContainer = data as SceneContainerScriptable));
            yield return StartCoroutine(HelperCoroutine.LoadDataFromResources("Scriptable/PoolSettings",
                (data) => PoolSetting = data as Pooler.PoolSettings));

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;

            yield return StartCoroutine(HelperCoroutine.LoadGameScene(SceneContainer.MainMenuScene));
            LoadingManager.Instance.HideLoadingScreen();
        }

        private void SceneManager_sceneUnloaded(Scene arg0)
        {
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
        }

        public void Play(int index)
        {
            StartCoroutine(LoadGameScene(index));
        }

        public void MainMenu()
        {
            StartCoroutine(LoadMainMenu());
        }

        private IEnumerator LoadMainMenu()
        {
            LoadingManager.Instance.ShowLoadingScreen();
            yield return StartCoroutine(HelperCoroutine.LoadGameScene(SceneContainer.MainMenuScene));
            LoadingManager.Instance.HideLoadingScreen();
        }

        private IEnumerator LoadGameScene(int index)
        {
            LoadingManager.Instance.ShowLoadingScreen();
            yield return StartCoroutine(HelperCoroutine.LoadGameScene(SceneContainer.GameLevelScenes[index]));
            EnemyBulletPooler.CreateInstance();
            PlayerBulletPooler.CreateInstance();
            LoadingManager.Instance.HideLoadingScreen();
        }
    }
}