using System;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class CooldownService : AbstractDataService<CooldownServiceData>, ICooldownService
    {
        private readonly ITimeService timeService;

        public float MaxCooldown { get; private set; }
        public float Cooldown { get; private set; }

        public event Action Finished;

        public bool IsFinished { get; private set; }

        [Inject]
        public CooldownService(ITimeService timeService, Scene scene) : base(scene)
        {
            this.timeService = timeService;
        }

        public void Update()
        {
            if (IsFinished)
            {
                return;
            }

            float epsilon = Scene.Settings.MathSettings.Epsilon;

            Cooldown -= timeService.DeltaTime;
            if (Cooldown.IsApproximatePositive(epsilon))
            {
                return;
            }
            Cooldown = 0.0f;

            IsFinished = true;

            Finished?.Invoke();
        }

        public void Reset()
        {
            Cooldown = MaxCooldown;

            IsFinished = false;
        }

        public void Finish()
        {
            Cooldown = 0.0f;

            IsFinished = true;
        }

        protected override void OnInitialized(CooldownServiceData data)
        {
            base.OnInitialized(data);

            float epsilon = Scene.Settings.MathSettings.Epsilon;

            MaxCooldown = data.MaxCooldown;
            Cooldown = data.Cooldown;

            if (Cooldown.IsApproximateZero(epsilon))
            {
                IsFinished = true;
            }
            else
            {
                IsFinished = false;
            }
        }
    }
}
