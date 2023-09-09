using System.Collections.Generic;
using System.Linq;

public class BulletMovementManager : BaseManager<BulletMovementManager>
{
    private List<IUpdatable> _bulletMovementControllers = new List<IUpdatable>();

    public void AddBullet(IUpdatable bullet)
    {
        _bulletMovementControllers.Add(bullet);
    }

    public void RemoveBullet(IUpdatable bullet)
    {
        _bulletMovementControllers.Remove(bullet);
    }

    private void FixedUpdate()
    {
        _bulletMovementControllers.ToList().ForEach(bullet => bullet.OnUpdate());
    }
}
