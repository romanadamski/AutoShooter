using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainGamePlaneController : BaseGamePlane
{
    [SerializeField]
    private ResizableController ground;

    private List<IUpdatable> _rotateShooters = new List<IUpdatable>();
    private uint _currentUniquePositionSearchAttempt;
    private Vector3 _shooterSize;

    private const uint UNIQUE_POSITION_SEARCH_MAX_ATTEMPTS = 50;

    public override Bounds GameBounds => ground.Bounds;

    private void Start()
    {
        SubscribeToEvents();
        SetShooterSize();
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.ShooterShoted += ShooterShoted;
    }

    private void SetShooterSize()
    {
        var tempShooter = ObjectPoolingManager.Instance.GetFromPool(GameObjectTagsConstants.SHOOTER);
        _shooterSize = tempShooter.GetComponent<ResizableController>().Bounds.size;
        ObjectPoolingManager.Instance.ReturnToPool(tempShooter);
    }

    private void ShooterShoted(uint lives, GameObject shooter)
    {
        if (lives > 0)
        {
            var timer = new TimerController(() => SpawnShooterInRandomPosition(shooter), TimerType.Transient);
            timer.SetInterval(GameSettingsManager.Instance.Settings.ShooterSpawnLatency);
            timer.StartCounting();
        }
    }

    public override void SpawnGameplayObjects()
    {
        var shooterCount = LevelSettingsManager.Instance.CurrentLevel.ObjectsCount;
        if (shooterCount == 0) return;

        _rotateShooters.Clear();


        ground.SetSize(new Vector3(_shooterSize.z * shooterCount, 0, _shooterSize.z * shooterCount));

        for (int i = 0; i < shooterCount; i++)
        {
            var shooter = ObjectPoolingManager.Instance.GetFromPool(GameObjectTagsConstants.SHOOTER);
            SpawnShooterInRandomPosition(shooter.gameObject);

            _rotateShooters.AddRange(shooter.GetComponents<IUpdatable>());
        }
    }

    private void SpawnShooterInRandomPosition(GameObject shooter)
    {
        _currentUniquePositionSearchAttempt = 0;

        var position = GetRandomUniquePosition();

        shooter.transform.position = position;
        shooter.gameObject.SetActive(true);
    }

    private Vector3 GetRandomUniquePosition()
    {
        var x = Random.Range(ground.Bounds.min.x + _shooterSize.x / 2,
            ground.Bounds.max.x - _shooterSize.x / 2);
        var z = Random.Range(ground.Bounds.min.z + _shooterSize.z / 2,
            ground.Bounds.max.z - _shooterSize.z / 2);

        var position = new Vector3(x, _shooterSize.y / 2, z);

        _currentUniquePositionSearchAttempt++;
        if (_currentUniquePositionSearchAttempt < UNIQUE_POSITION_SEARCH_MAX_ATTEMPTS)
        {
            var colliders = Physics.OverlapSphere(position, _shooterSize.z).Where(x => x.tag.Equals(GameObjectTagsConstants.SHOOTER));

            if (colliders.Any())
            {
                position = GetRandomUniquePosition();
            }
        }
        else
        {
            Debug.Log($"Too many attemps!");
            return position;
        }

        return position;
    }

    private void Update()
    {
        if (!GameplayManager.Instance.DuringGameplayState) return;

        _rotateShooters.ForEach(shooter => shooter.OnUpdate());
    }
}
