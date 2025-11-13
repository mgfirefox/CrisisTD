using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class BasePresenter : AbstractUiPresenter<BaseData, IBaseView, IBaseUi>, IBasePresenter
    {
        private readonly IHealthService healthService;

        [Inject]
        public BasePresenter(IBaseView view, IBaseUi ui, IHealthService healthService, Scene scene)
            : base(view, ui, scene)
        {
            this.healthService = healthService;
        }

        public override void OnSceneStarted()
        {
            base.OnSceneStarted();

            View.DamageTaken += OnDamageTaken;
            View.Died += OnDied;

            healthService.Died += OnHealthDied;
        }

        public override void OnSceneFinished()
        {
            base.OnSceneFinished();

            View.DamageTaken -= OnDamageTaken;
            View.Died -= OnDied;

            healthService.Died -= OnHealthDied;
        }

        protected override void OnInitialized(BaseData data)
        {
            base.OnInitialized(data);

            healthService.Initialize(data.HealthServiceData);

            View.MaxHealth = healthService.MaxHealth;
            View.Health = healthService.Health;

            Ui.SetHealth(healthService.Health, healthService.MaxHealth);
        }

        protected override void OnDestroying()
        {
            healthService.Destroy();

            base.OnDestroying();
        }

        private void OnHealthDied()
        {
            View.Die();

            View.Health = healthService.Health;
            View.IsDied = healthService.IsDied;

            Ui.SetHealth(healthService.Health, healthService.MaxHealth);

            Destroy();
        }

        private void OnDamageTaken(IEnemyView enemy)
        {
            healthService.TakeDamage(enemy.Health);

            View.Health = healthService.Health;

            Ui.SetHealth(healthService.Health, healthService.MaxHealth);

            enemy.Die();
        }

        private void OnDied()
        {
            Debug.LogWarning("Base died");

            healthService.Die();
        }
    }
}
