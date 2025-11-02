using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractAttackAction<TData, TIView> : AbstractAction<TData, TIView>,
        IAttackAction
        where TData : AbstractAttackActionData
        where TIView : class, IAttackActionView
    {
        protected IEnemyTargetService TargetService { get; }

        protected ICooldownService CooldownService { get; }

        public float Damage { get; private set; }

        protected AbstractAttackAction(TIView view, IEnemyTargetService targetService,
            ICooldownService cooldownService, Scene scene) : base(view, scene)
        {
            TargetService = targetService;
            CooldownService = cooldownService;
        }

        public override void Perform()
        {
            CooldownService.Update();

            View.Cooldown = CooldownService.Cooldown;

            if (!CooldownService.IsFinished)
            {
                return;
            }

            IReadOnlyList<IEnemyView> targets = TargetService.Targets;
            if (targets.Count == 0)
            {
                return;
            }

            PerformAttack(targets);

            CooldownService.Reset();

            View.Cooldown = CooldownService.Cooldown;
        }

        protected abstract void PerformAttack(IReadOnlyList<IEnemyView> targets);

        protected override void OnInitialized(TData data)
        {
            base.OnInitialized(data);

            Damage = data.Damage;

            View.Damage = Damage;

            CooldownService.Initialize(data.CooldownServiceData);

            View.MaxCooldown = CooldownService.MaxCooldown;
            View.Cooldown = CooldownService.Cooldown;
        }

        protected override void OnDestroying()
        {
            CooldownService.Destroy();

            base.OnDestroying();
        }
    }
}
