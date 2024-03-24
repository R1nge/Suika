namespace _Assets.Scripts.Gameplay
{
    public class SuikaDestroyer : Suika
    {
        protected override void OnCollision(Suika other)
        {
            CollisionService.OnCollisionDestroy(this, other);
        }
    }
}