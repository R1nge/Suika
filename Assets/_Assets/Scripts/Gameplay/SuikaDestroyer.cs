namespace _Assets.Scripts.Gameplay
{
    public class SuikaDestroyer : Suika
    {
        protected override void OnCollision(Suika suika)
        {
            ResetService.RemoveSuika(this);
            ResetService.RemoveSuika(suika);
            Destroy(gameObject);
            Destroy(suika.gameObject);
        }
    }
}