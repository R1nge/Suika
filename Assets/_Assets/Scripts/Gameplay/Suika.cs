using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class Suika : MonoBehaviour
    {
        //TODO: when suikas collide, destroy them and spawn one at the middle of them (between) with almost zero scale and scale it up to the original size
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private PolygonColliderOptimizer polygonColliderOptimizer;
        public PolygonColliderOptimizer PolygonColliderOptimizer => polygonColliderOptimizer;
        public bool HasLanded => _landed;
        public bool HasDropped => _dropped;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        protected internal int Index;
        protected internal bool Collided;
        private bool _landed;
        private bool _dropped;
        [Inject] protected SuikasFactory SuikasFactory;
        [Inject] protected ScoreService ScoreService;
        [Inject] protected ResetService ResetService;


        public void SetIndex(int index) => Index = index;

        public void Drop() => _dropped = true;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_dropped)
            {
                _landed = true;
                if (other.gameObject.TryGetComponent(out Suika suika))
                {
                    if (suika.HasDropped)
                    {
                        OnCollision(suika);    
                    }
                }
            }
        }

        protected virtual void OnCollision(Suika suika)
        {
            if (suika.Index == Index)
            {
                if (Collided || suika.Collided) return;
                Collided = true;
                suika.Collided = true;
                var middle = (transform.position + suika.transform.position) / 2f;
                //Or move it to the another suika position
                //var suikaPosition = suika.transform.position;
                //newSuikaInstance.transform.position = suikaPosition;
                SuikasFactory.Create(Index, middle);
                ResetService.RemoveSuika(this);
                ResetService.RemoveSuika(suika);
                Destroy(gameObject);
                Destroy(suika.gameObject);
            }
        }

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
            spriteRenderer.size = new Vector2(256, 256);
        }
    }
}