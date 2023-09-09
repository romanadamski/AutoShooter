using System.Collections.Generic;
using System.Linq;

public class BulletMovementManager : BaseManager<BulletMovementManager>
{
    List<IUpdatable> bulletMovementControllers = new List<IUpdatable>();

    public void AddBullet(IUpdatable bullet)
    {
        bulletMovementControllers.Add(bullet);
    }

    public void RemoveBullet(IUpdatable bullet)
    {
        bulletMovementControllers.Remove(bullet);
    }

    private void FixedUpdate()
    {
        bulletMovementControllers.ToList().ForEach(bullet => bullet.OnUpdate());
    }
}
