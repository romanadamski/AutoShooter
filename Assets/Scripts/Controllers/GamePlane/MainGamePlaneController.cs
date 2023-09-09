using System.Collections.Generic;
using UnityEngine;

public class MainGamePlaneController : BaseGamePlane
{
    [SerializeField]
    private ResizableController ground;

    private List<IUpdatable> _rotateShooters = new List<IUpdatable>();

    public override Bounds GameBounds => ground.Bounds;

    private void Start()
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.ShooterShoted += ShooterShoted;
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

        ground.SetSize(new Vector3(Mathf.Pow(shooterCount, 1f), 0 , Mathf.Pow(shooterCount, 1f)));
        _rotateShooters.Clear();

        for (int i = 0; i < shooterCount; i++)
        {
            var shooter = ObjectPoolingManager.Instance.GetFromPool(GameObjectTagsConstants.SHOOTER);
            SpawnShooterInRandomPosition(shooter.gameObject);

            _rotateShooters.AddRange(shooter.GetComponents<IUpdatable>());
        }
    }

    //todo do not touch each other
    private void SpawnShooterInRandomPosition(GameObject shooter)
    {
        var resizebleShooter = shooter.GetComponent<ResizableController>();
        var x = Random.Range(ground.Bounds.min.x + resizebleShooter.Bounds.size.x / 2,
            ground.Bounds.max.x - resizebleShooter.Bounds.size.x / 2);
        var z = Random.Range(ground.Bounds.min.z + resizebleShooter.Bounds.size.z / 2,
            ground.Bounds.max.z - resizebleShooter.Bounds.size.z / 2);

        shooter.transform.position = new Vector3(x, resizebleShooter.Bounds.size.y / 2, z);
        shooter.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!GameplayManager.Instance.DuringGameplayState) return;

        _rotateShooters.ForEach(shooter => shooter.OnUpdate());
    }
}
