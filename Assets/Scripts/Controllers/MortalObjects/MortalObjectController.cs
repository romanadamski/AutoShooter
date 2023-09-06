using UnityEngine;

public class MortalObjectController : BaseMortalObjectController
{
    private const float POSITION_RANDOMIZE_RANGE = 0.8f;

    [SerializeField]
    private SerializableTuple<int, int> pieceCountRange;
    [SerializeField]
    private string pieceType;

    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.BULLET };
    }

    protected override void OnTriggerWithEnemyEnter(Collider collider)
    {
        ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
        var randomPieceCount = Random.Range(pieceCountRange.Item1, pieceCountRange.Item2 + 1);

        for (int i = 0; i < randomPieceCount; i++)
        {
            var shooter = ObjectPoolingManager.Instance.GetFromPool(pieceType);

            var randomPosition = new Vector2(
                Random.Range(transform.position.x - POSITION_RANDOMIZE_RANGE, transform.position.x + POSITION_RANDOMIZE_RANGE),
                Random.Range(transform.position.y - POSITION_RANDOMIZE_RANGE, transform.position.y + POSITION_RANDOMIZE_RANGE));
            shooter.transform.position = randomPosition;
        }

        EventsManager.Instance.OnObjectShotted();
    }
}
