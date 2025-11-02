using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class BasePresenter : AbstractPresenter<BaseData, IBaseView>, IBasePresenter
    {
        private readonly IHealthService healthService;

        [Inject]
        public BasePresenter(IBaseView view, IHealthService healthService, Scene scene) : base(view,
            scene)
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

            Destroy();
        }

        private void OnDamageTaken(IEnemyView enemy)
        {
            healthService.TakeDamage(enemy.Health);

            View.Health = healthService.Health;

            enemy.Die();
        }

        private void OnDied()
        {
            Debug.LogWarning("Base died");

            healthService.Die();
        }
    }
}
