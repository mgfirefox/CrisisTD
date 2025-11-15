using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerModelService : AbstractDataService<TowerModelServiceData>, ITowerModelService, ISceneStartedListener, ISceneFinishedListener
    {
        private readonly ILevelService levelService;
        
        private readonly IDictionary<BranchLevel, IModelComponent> models = new Dictionary<BranchLevel, IModelComponent>();
        
        public IModelComponent Model { get; private set; }

        public event Action<IModelComponent> Changed;
        
        [Inject]
        public TowerModelService(ILevelService levelService, Scene scene) : base(scene)
        {
            this.levelService = levelService;
        }

        public void OnSceneStarted()
        {
            levelService.Changed += OnLevelChanged;
        }

        public void OnSceneFinished()
        {
            levelService.Changed -= OnLevelChanged;
        }

        protected override void OnInitialized(TowerModelServiceData data)
        {
            base.OnInitialized(data);
            
            InitializeModels(data.Models);
        }

        private void InitializeModels(IDictionary<BranchLevel, IModelComponent> models)
        {
            this.models.AddRange(models);
        }

        protected override void OnDestroying()
        {
            ClearModels();
            
            base.OnDestroying();
        }

        private void ClearModels()
        {
            models.Clear();
        }

        private void OnLevelChanged(LevelItem item)
        {
            if (models.TryGetValue(item.Level, out IModelComponent model))
            {
                Changed?.Invoke(model);

                foreach (IModelComponent model1 in models.Values)
                {
                    if (model1 == model)
                    {
                        continue;
                    }
                    
                    model1.Transform.gameObject.SetActive(false);
                }

                Model = model;
                Model.Transform.gameObject.SetActive(true);
            }
        }
    }
}
