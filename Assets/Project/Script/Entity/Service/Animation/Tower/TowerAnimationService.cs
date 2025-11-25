using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class TowerAnimationService : AbstractDataService<TowerAnimationServiceData>, ITowerAnimationService, ISceneStartedListener, ISceneFinishedListener
    {
        private readonly ITowerModelService modelService;
        
        private readonly IList<IAnimatorComponent> animators = new List<IAnimatorComponent>();

        [Inject]
        private TowerLifetimeScope lifetimeScope;
        [Inject]
        private Tracer tracerPrefab;
        [Inject]
        private Explosion explosionPrefab;

        [Inject]
        public TowerAnimationService(ITowerModelService modelService, Scene scene) : base(scene)
        {
            this.modelService = modelService;
        }

        public void OnSceneStarted()
        {
            modelService.Changed += OnModelChanged;
        }

        public void OnSceneFinished()
        {
            modelService.Changed -= OnModelChanged;
        }

        public void SetBool(string name, bool value)
        {
            foreach (IAnimatorComponent animator in animators)
            {
                animator.SetBool(name, value);
            }
        }

        public void SetFloat(string name, float value)
        {
            foreach (IAnimatorComponent animator in animators)
            {
                animator.SetFloat(name, value);
            }
        }
        
        public void SetInt(string name, int value)
        {
            foreach (IAnimatorComponent animator in animators)
            {
                animator.SetInt(name, value);
            }
        }

        public void ActivateTrigger(string name)
        {
            foreach (IAnimatorComponent animator in animators)
            {
                animator.ActivateTrigger(name);
            }
        }
        
        public void CreateTracer(Vector3 endPosition)
        {
            Tracer tracer = lifetimeScope.Container.Instantiate(tracerPrefab, animators[0].Transform.parent);
            tracer.EndPosition = endPosition;
        }
        
        public void CreateExplosion(Vector3 position, float radius)
        {
            Explosion explosion = lifetimeScope.Container.Instantiate(explosionPrefab, animators[0].Transform.parent);
            explosion.Position = position;
            explosion.Radius = radius;
        }

        protected override void OnDestroying()
        {
            ClearAnimators();
            
            base.OnDestroying();
        }

        private void ClearAnimators()
        {
            animators.Clear();
        }

        private void OnModelChanged(IModelComponent model)
        {
            ClearAnimators();
            
            animators.AddRange(model.AnimatorFolder.Children);
        }
    }
}
