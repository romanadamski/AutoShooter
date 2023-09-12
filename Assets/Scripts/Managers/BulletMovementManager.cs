using System.Collections.Generic;
using System.Linq;

public class BulletMovementManager : BaseManager<BulletMovementManager>
{
    private Dictionary<int, IUpdatable> _bulletMovementControllers = new Dictionary<int, IUpdatable>();

    public void AddBullet(int id, IUpdatable bullet)
    {
        _bulletMovementControllers.TryAdd(id, bullet);
    }

    public void RemoveBullet(int id)
    {
        _bulletMovementControllers.Remove(id);
    }

    private void FixedUpdate()
    {
        _bulletMovementControllers.ToList().ForEach(bullet => bullet.Value.OnUpdate());
    }
}
