using UnityEngine;

namespace BulletJam.Helper
{
    [CreateAssetMenu(menuName = "Scriptable/SceneContainer")]
    public class SceneContainerScriptable : ScriptableObject
    {
        [field: SerializeField, SceneName] public string SplashScene { get; private set; }
        [field: SerializeField, SceneName] public string MainMenuScene { get; private set; }
        [field: SerializeField, SceneName] public string[] GameLevelScenes { get; private set; }
    }
}