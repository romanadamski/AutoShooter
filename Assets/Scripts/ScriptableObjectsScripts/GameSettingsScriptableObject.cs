using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings")]
public class GameSettingsScriptableObject : ScriptableObject
{
    [Header("Shooter settings")]

    [SerializeField]
    private float shooterRotationSpeed;
    public float ShooterRotationSpeed => shooterRotationSpeed;
    [SerializeField]
    private SerializableTuple<float, float> rotatingTimeFrequency;
    public SerializableTuple<float, float> RotatingTimeFrequency => rotatingTimeFrequency;

    [Header("Bullet settings")]

    [SerializeField]
    private float baseBulletMovementSpeed;
    public float BaseBulletMovementSpeed => baseBulletMovementSpeed;
}
