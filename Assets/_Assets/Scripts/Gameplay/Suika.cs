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
        public bool HasLanded => _landed;
        protected internal int Index;
        protected internal bool Collided;
        private bool _landed;
        [Inject] protected SuikasFactory SuikasFactory;
        [Inject] protected ScoreService ScoreService;
        [Inject] protected ResetService ResetService;


        public void SetIndex(int index) => Index = index;

        private void OnCollisionEnter2D(Collision2D other)
        {
            _landed = true;
            if (other.gameObject.TryGetComponent(out Suika suika))
            {
                OnCollision(suika);
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
    }
}