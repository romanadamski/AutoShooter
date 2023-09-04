using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level Settings")]
public class LevelSettingsScriptableObject : ScriptableObject
{
    [SerializeField]
    private uint levelNumber;
    public uint LevelNumber => levelNumber;
    [SerializeField]
    private uint objectsCount;
    public uint ObjectsCount => objectsCount;
    [SerializeField]
    private Color imageColor;
    public Color ImageColor => imageColor;

    [Header("Shooter settings")]
    [SerializeField]
    private SerializableTuple<float, float> releasingFrequency;
    public SerializableTuple<float, float> ReleasingFrequency => releasingFrequency;

    [SerializeField]
    private SerializableTuple<float, float> speedRange;
    public SerializableTuple<float, float> SpeedRange => speedRange;
}
