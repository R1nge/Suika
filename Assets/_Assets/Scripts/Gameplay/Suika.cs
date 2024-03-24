using _Assets.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class Suika : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private PolygonColliderOptimizer polygonColliderOptimizer;
        public PolygonColliderOptimizer PolygonColliderOptimizer => polygonColliderOptimizer;
        public bool HasLanded => _landed;
        public bool HasDropped => _dropped;

        public bool HasCollided
        {
            get => _collided;
            set => _collided = value;
        }

        public SpriteRenderer SpriteRenderer => spriteRenderer;
        protected internal int Index;
        private bool _landed;
        private bool _dropped;
        private bool _collided;
        [Inject] protected ContinueGameService ContinueGameService;
        [Inject] protected CollisionService CollisionService;

        public void SetIndex(int index) => Index = index;

        public void Drop()
        {
            _dropped = true;
            ContinueGameService.AddSuika(this);
        }

        public void Land() => _landed = true;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_dropped) return;

            if (_collided) return;

            _landed = true;

            if (!other.gameObject.TryGetComponent(out Suika suika)) return;

            if (!suika.HasDropped) return;

            OnCollision(suika);
        }

        protected virtual void OnCollision(Suika other)
        {
            if (other.Index != Index) return;
            other.HasCollided = true;
            _collided = true;
            CollisionService.OnCollision(this, other);
        }

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
            spriteRenderer.size = new Vector2(256, 256);
        }
    }
}