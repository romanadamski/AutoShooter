using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings")]
public class GameSettingsScriptableObject : ScriptableObject
{
    [Header("Game settings")]

    [SerializeField]
    private uint minShootersCount;
    public uint MinShootersCount => minShootersCount;

    [Header("Shooter settings")]

    [SerializeField]
    private float shooterSpawnLatency;
    public float ShooterSpawnLatency => shooterSpawnLatency;
    [SerializeField]
    private uint shooterLives;
    public uint ShooterLives => shooterLives;
    [SerializeField]
    private float shooterRotationSpeed;
    public float ShooterRotationSpeed => shooterRotationSpeed;
    [SerializeField]
    private SerializableTuple<float, float> rotatingTimeFrequency;
    public SerializableTuple<float, float> RotatingTimeFrequency => rotatingTimeFrequency;

    [Header("Bullet settings")]

    [SerializeField]
    private float bulletMovementSpeed;
    public float BulletMovementSpeed => bulletMovementSpeed;
}
