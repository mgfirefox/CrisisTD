using System.Collections.Generic;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractAttackAction<TData, TIView> : AbstractAction<TData, TIView>,
        IAttackAction
        where TData : AbstractAttackActionData
        where TIView : class, IAttackActionView
    {
        protected const string attackTriggerName = "Attack";
        protected const string hasTargetBoolName = "HasTarget";
        
        protected ITowerTransformService TransformService { get; }
        // TODO: Remove it when ReactiveProperty will be used
        [Inject]
        protected ITowerView TowerView { get; private set; }
        
        protected ITowerAnimationService AnimationService { get; private set; }

        protected IEnemyTargetService TargetService { get; }

        protected ICooldownService CooldownService { get; }

        public float Damage { get; private set; }
        public float ArmorPiercing { get; private set; }

        protected AbstractAttackAction(TIView view, IEnemyTargetService targetService,
            ICooldownService cooldownService, ITowerTransformService transformService, ITowerAnimationService animationService, Scene scene) : base(view, scene)
        {
            TargetService = targetService;
            CooldownService = cooldownService;
            TransformService = transformService;
            AnimationService = animationService;
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
                AnimationService.SetBool(hasTargetBoolName, false);
                
                return;
            }
            
            AnimationService.SetBool(hasTargetBoolName, true);

            PerformAttack(targets);

            CooldownService.Reset();

            View.Cooldown = CooldownService.Cooldown;
        }

        protected virtual void PerformAttack(IReadOnlyList<IEnemyView> targets)
        {
            IEnemyView target = targets[0];

            TransformService.RotateTo(target);
            
            TowerView.VisualOrientation = TransformService.Orientation;
            
            AnimationService.ActivateTrigger(attackTriggerName);
        }

        protected override void OnInitialized(TData data)
        {
            base.OnInitialized(data);

            Damage = data.Damage;
            ArmorPiercing = data.ArmorPiercing;

            View.Damage = Damage;
            View.ArmorPiercing = data.ArmorPiercing;

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
