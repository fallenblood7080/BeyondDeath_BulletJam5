using System.Collections;
using UnityEngine;
using UnitySingleton;

namespace BulletJam.Core
{
    using Helper;

    public class LoadingManager : PersistentMonoSingleton<LoadingManager>
    {
        [SerializeField, Disable] private GameObject loadingScreen;

        public IEnumerator GetLoadingScreenObject()
        {
            GameObject loadingScreenPrefab = null;

            yield return StartCoroutine(HelperCoroutine.LoadDataFromResources("Prefabs/LoadingCanvas", (data) => loadingScreenPrefab = data as GameObject));

            if (loadingScreenPrefab == null)
            {
                Logger.Instance.Log("LoadingCanvas is not found", LogGroup.Gameplay, this);
                yield break;
            }

            loadingScreen = Instantiate(loadingScreenPrefab);
            DontDestroyOnLoad(loadingScreen);
            loadingScreen.SetActive(false);
            yield return null;
        }

        public void ShowLoadingScreen()
        {
            if (!loadingScreen.activeInHierarchy)
            {
                loadingScreen.SetActive(true);
                loadingScreen.GetComponent<CanvasGroup>().interactable = true;
                loadingScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }

        public void HideLoadingScreen()
        {
            if (loadingScreen.activeInHierarchy)
            {
                loadingScreen.SetActive(false);
                loadingScreen.GetComponent<CanvasGroup>().interactable = false;
                loadingScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }
}