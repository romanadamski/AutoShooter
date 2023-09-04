public class SimpleBulletMovementController : BaseBulletMovementController
{
    protected override void OnOutsideScreen()
    {
        DeactivateMovingObject();
    }
}
