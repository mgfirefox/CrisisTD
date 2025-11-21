using System;
using System.Collections.Generic;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class BurstAttackAction :
        AbstractAttackAction<BurstAttackActionData, IBurstAttackActionView>, IBurstAttackAction
    {
        private readonly ICooldownService burstCooldownService;

        public int MaxBurstShotCount { get; private set; }
        public int BurstShotCount { get; private set; }

        [Inject]
        public BurstAttackAction(IBurstAttackActionView view, IEnemyTargetService targetService,
            [Key("Burst")] ICooldownService burstCooldownService, ICooldownService cooldownService, ITowerTransformService transformService, ITowerAnimationService animationService,
            Scene scene) : base(view, targetService, cooldownService, transformService, animationService, scene)
        {
            this.burstCooldownService = burstCooldownService;
        }

        public override void Perform()
        {
            burstCooldownService.Update();

            View.BurstCooldown = burstCooldownService.Cooldown;

            if (!burstCooldownService.IsFinished)
            {
                return;
            }

            IReadOnlyList<IEnemyView> targets = TargetService.Targets;
            if (targets.Count == 0)
            {
                if (BurstShotCount == 0)
                {
                    return;
                }

                ResetBurst();

                return;
            }

            PerformAttack(targets);

            if (BurstShotCount < MaxBurstShotCount)
            {
                return;
            }
            if (BurstShotCount > MaxBurstShotCount)
            {
                // TODO: Change Exception
                throw new Exception(
                    $"Fired {BurstShotCount} shots that more than must be ({MaxBurstShotCount}).");
            }

            ResetBurst();
        }

        protected override void PerformAttack(IReadOnlyList<IEnemyView> targets)
        {
            base.PerformAttack(targets);
            
            CooldownService.Update();

            View.Cooldown = CooldownService.Cooldown;

            if (!CooldownService.IsFinished)
            {
                return;
            }

            IEnemyView target = targets[0];
            target.TakeDamage(Damage);

            BurstShotCount++;

            View.BurstShotCount = BurstShotCount;

            CooldownService.Reset();

            View.Cooldown = CooldownService.Cooldown;
        }

        private void ResetBurst()
        {
            BurstShotCount = 0;

            View.BurstShotCount = BurstShotCount;

            burstCooldownService.Reset();

            View.BurstCooldown = burstCooldownService.Cooldown;

            CooldownService.Finish();

            View.Cooldown = CooldownService.Cooldown;
        }

        protected override void OnInitialized(BurstAttackActionData data)
        {
            base.OnInitialized(data);

            MaxBurstShotCount = data.MaxBurstShotCount;
            BurstShotCount = data.BurstShotCount;

            View.MaxBurstShotCount = MaxBurstShotCount;
            View.BurstShotCount = BurstShotCount;

            burstCooldownService.Initialize(data.BurstCooldownServiceData);

            View.BurstMaxCooldown = burstCooldownService.MaxCooldown;
            View.BurstCooldown = burstCooldownService.Cooldown;
        }

        protected override void OnDestroying()
        {
            burstCooldownService.Destroy();

            base.OnDestroying();
        }
    }
}
