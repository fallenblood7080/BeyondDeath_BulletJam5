using UnityEngine;

namespace BulletJam.Helper
{
    [CreateAssetMenu(fileName = "LoggerSettings", menuName = "Scriptable/LoggerSettings")]
    public class LoggerSettings : ScriptableObject
    {
        [field: SerializeField, LabelByChild("group")] public LogData[] Datas { get; private set; }
    }

    [System.Serializable]
    public struct LogData
    {
        [field: SerializeField] public LogGroup group;
        [field: SerializeField] public Color color;
        [field: SerializeField] public int fontSize;
    }

    public enum LogGroup
    {
        None,
        Player,
        Enemy,
        Gameplay,
    }
}