using System.Collections.Generic;
using Unity.VisualScripting;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerAnimationService : AbstractDataService<TowerAnimationServiceData>, ITowerAnimationService, ISceneStartedListener, ISceneFinishedListener
    {
        private readonly ITowerModelService modelService;
        
        private readonly IList<IAnimatorComponent> animators = new List<IAnimatorComponent>();

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
