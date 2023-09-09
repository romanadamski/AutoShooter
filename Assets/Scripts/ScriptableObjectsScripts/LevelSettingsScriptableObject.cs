using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level Settings")]
public class LevelSettingsScriptableObject : ScriptableObject
{
    [SerializeField]
    private uint levelNumber;
    public uint LevelNumber => levelNumber;

    [SerializeField]
    private uint shootersCount;
    public uint ShootersCount => shootersCount;

    [SerializeField]
    private Color imageColor;
    public Color ImageColor => imageColor;
}
