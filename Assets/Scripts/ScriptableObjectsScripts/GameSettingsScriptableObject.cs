using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings")]
public class GameSettingsScriptableObject : ScriptableObject
{
    [Header("Player settings")]
    [SerializeField]
    private float playerMovementSpeed;
    public float PlayerMovementSpeed => playerMovementSpeed;

    [SerializeField]
    private float playerRotationSpeed;
    public float PlayerRotationSpeed => playerRotationSpeed;

    [SerializeField]
    private float playerMovementPrecision;
    public float PlayerMovementPrecision => playerMovementPrecision;

    [Header("Bullet settings")]
    [SerializeField]
    private float baseBulletMovementSpeed;
    public float BaseBulletMovementSpeed => baseBulletMovementSpeed;

    [Header("Game settings")]
    [SerializeField]
    private float _idleStateTime;
    public float IdleStateTime => _idleStateTime;

    [SerializeField]
    private int _maxHighscoresSaveCount;
    public int MaxHighscoresSaveCount => _maxHighscoresSaveCount;
}
