using UnityEngine;

namespace BulletJam.Pooler
{
    [CreateAssetMenu(fileName = "PoolSettings", menuName = "Scriptable/PoolSettings")]
    public class PoolSettings : ScriptableObject
    {
        [field: SerializeField, LabelByChild("PoolName")] public PoolData[] datas { get; private set; }
    }

    [System.Serializable]
    public struct PoolData
    {
        [field: SerializeField] public string PoolName;
        [field: SerializeField, PrefabObjectOnly, NotNull] public GameObject PooledObject;
        [field: SerializeField, BeginHorizontal] public int Size;
        [field: SerializeField, EndHorizontal] public int MaxSize;
    }
}