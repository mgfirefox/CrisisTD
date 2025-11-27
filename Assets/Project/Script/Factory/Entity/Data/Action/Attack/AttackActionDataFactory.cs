using System;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class AttackActionDataFactory : AbstractDataFactory, IAttackActionDataFactory
    {
        [Inject]
        public AttackActionDataFactory()
        {
        }

        public AbstractAttackActionData Create(AbstractAttackActionDataConfiguration configuration)
        {
            if (!AttackActionTypeValidator.TryValidate(configuration.Type))
            {
                throw new InvalidArgumentException(nameof(configuration), configuration.ToString());
            }

            AbstractAttackActionData data;
            switch (configuration.Type)
            {
                case AttackActionType.SingleTarget:
                    if (configuration is SingleTargetAttackActionDataConfiguration)
                    {
                        data = new SingleTargetAttackActionData();

                        break;
                    }

                    throw new InvalidArgumentException(nameof(configuration),
                        configuration.ToString());
                case AttackActionType.Area:
                    if (configuration is AreaAttackActionDataConfiguration areaConfiguration)
                    {
                        data = new AreaAttackActionData
                        {
                            InnerRadius = areaConfiguration.InnerRadius,
                            OuterRadius = areaConfiguration.OuterRadius,
                            MaxHitCount = areaConfiguration.MaxHitCount,
                        };

                        break;
                    }

                    throw new InvalidArgumentException(nameof(configuration),
                        configuration.ToString());
                case AttackActionType.ArcAngle:
                    if (configuration is ArcAngleAttackActionDataConfiguration
                        arcAngleConfiguration)
                    {
                        data = new ArcAngleAttackActionData
                        {
                            ArcAngle = arcAngleConfiguration.ArcAngle,
                            PelletCount = arcAngleConfiguration.PelletCount,
                            MaxPelletHitCount = arcAngleConfiguration.MaxPelletHitCount,
                        };

                        break;
                    }

                    throw new InvalidArgumentException(nameof(configuration),
                        configuration.ToString());
                case AttackActionType.Burst:
                    if (configuration is BurstAttackActionDataConfiguration burstConfiguration)
                    {
                        data = new BurstAttackActionData
                        {
                            MaxBurstShotCount = burstConfiguration.BurstShotCount,
                            BurstCooldownServiceData =
                            {
                                MaxCooldown = burstConfiguration.BurstCooldown,
                            },
                        };

                        break;
                    }

                    throw new InvalidArgumentException(nameof(configuration),
                        configuration.ToString());
                case AttackActionType.Undefined:
                default:
                    throw new InvalidArgumentException(nameof(configuration),
                        configuration.ToString());
            }

            data.Damage = configuration.Damage;
            data.ArmorPiercing = configuration.ArmorPiercing;
            data.CooldownServiceData.MaxCooldown = configuration.FireRate;

            return data;
        }

        public bool TryCreate(AbstractAttackActionDataConfiguration configuration,
            out AbstractAttackActionData data)
        {
            try
            {
                data = Create(configuration);

                return true;
            }
            catch (Exception e)
            {
                if (e is not InvalidArgumentException)
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            data = null;

            return false;
        }
    }
}
