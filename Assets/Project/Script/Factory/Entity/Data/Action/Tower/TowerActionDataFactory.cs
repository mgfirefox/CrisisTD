using System;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerActionDataFactory : AbstractDataFactory, ITowerActionDataFactory
    {
        private readonly IAttackActionDataFactory attackActionDataFactory;
        private readonly IBuffActionDataFactory buffActionDataFactory;

        [Inject]
        public TowerActionDataFactory(IAttackActionDataFactory attackActionDataFactory,
            IBuffActionDataFactory buffActionDataFactory)
        {
            this.attackActionDataFactory = attackActionDataFactory;
            this.buffActionDataFactory = buffActionDataFactory;
        }

        public AbstractTowerActionData Create(TowerType type,
            AbstractTowerActionDataConfiguration configuration, TargetPriority targetPriority)
        {
            if (!TowerTypeValidator.TryValidate(type))
            {
                throw new InvalidArgumentException(nameof(type), type.ToString());
            }

            switch (type)
            {
                case TowerType.Attack:
                    var attackConfiguration =
                        configuration.Cast<AttackTowerActionDataConfiguration>();

                    return CreateAttack(attackConfiguration, targetPriority);
                case TowerType.Support:
                    var supportConfiguration =
                        configuration.Cast<SupportTowerActionDataConfiguration>();

                    return CreateSupport(supportConfiguration, targetPriority);
                case TowerType.Undefined:
                default:
                    throw new InvalidArgumentException(nameof(type), type.ToString());
            }
        }

        public bool TryCreate(TowerType type, AbstractTowerActionDataConfiguration configuration,
            TargetPriority targetPriority, out AbstractTowerActionData data)
        {
            try
            {
                data = Create(type, configuration, targetPriority);

                return true;
            }
            catch (Exception e)
            {
                if (e is not (InvalidArgumentException or PrefabNotFoundException))
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            data = null;

            return false;
        }

        private AttackTowerActionData CreateAttack(
            AttackTowerActionDataConfiguration attackConfiguration, TargetPriority targetPriority)
        {
            var attackData = new AttackTowerActionData();

            foreach (AbstractAttackActionDataConfiguration attackActionConfiguration in
                     attackConfiguration.ActionDataConfigurations)
            {
                if (attackActionDataFactory.TryCreate(attackActionConfiguration,
                        out AbstractAttackActionData attackActionData))
                {
                    attackData.ActionDataList.Add(attackActionData);
                }
                else
                {
                    // TODO: Change Warning
                    Debug.LogWarning(
                        $"Failed to create Object of type {typeof(AbstractAttackActionData)} with ID \"${attackActionConfiguration.Type}\"");
                }
            }

            InitializeAbstract(attackData, attackConfiguration, targetPriority);

            return attackData;
        }

        private SupportTowerActionData CreateSupport(
            SupportTowerActionDataConfiguration supportConfiguration, TargetPriority targetPriority)
        {
            var supportData = new SupportTowerActionData();

            foreach (AbstractBuffActionDataConfiguration supportActionConfiguration in
                     supportConfiguration.ActionDataConfigurations)
            {
                if (buffActionDataFactory.TryCreate(supportActionConfiguration,
                        out AbstractBuffActionData buffActionData))
                {
                    supportData.ActionDataList.Add(buffActionData);
                }
                else
                {
                    // TODO: Change Warning
                    Debug.LogWarning(
                        $"Failed to create Object of type {typeof(AbstractBuffActionData)} with ID \"${supportActionConfiguration.Type}\"");
                }
            }

            InitializeAbstract(supportData, supportConfiguration, targetPriority);

            return supportData;
        }

        private void InitializeAbstract(AbstractTowerActionData data,
            AbstractTowerActionDataConfiguration configuration, TargetPriority targetPriority)
        {
            data.TargetServiceData.RangeRadius = configuration.Range;
            data.TargetServiceData.TargetPriority = targetPriority;
        }
    }
}
