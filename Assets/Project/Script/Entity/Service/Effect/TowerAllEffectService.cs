using System.Collections.Generic;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerAllEffectService :
        AbstractAllEffectService<TowerAllEffectServiceData, ITowerView>, ITowerAllEffectService,
        ISceneStartedListener, ISceneFinishedListener
    {
        private readonly ITowerBuffEffectService buffService;

        [Inject]
        public TowerAllEffectService(ITowerBuffEffectService buffService, Scene scene) : base(scene)
        {
            this.buffService = buffService;
        }

        public void OnSceneStarted()
        {
            buffService.Changed += OnEffectChanged;
        }

        public void OnSceneFinished()
        {
            buffService.Changed -= OnEffectChanged;
        }

        protected override void ApplyBuff(BuffEffect buffEffect, ITowerView source)
        {
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            base.ApplyBuff(buffEffect, source);

            buffService.Apply(buffEffect, source, epsilon);
        }

        protected override void OnEffectChanged<TEffect>(TEffect effect)
        {
            base.OnEffectChanged(effect);

            IList<Effect> effects = new List<Effect>
            {
                buffService.Get(effect.Type),
            };

            Effect newEffect = CalculateNewEffect(effects);

            InvokeEffectChanged(newEffect.Clone() as Effect);
        }

        protected override void OnDestroying()
        {
            buffService.Clear();

            base.OnDestroying();
        }
    }
}
