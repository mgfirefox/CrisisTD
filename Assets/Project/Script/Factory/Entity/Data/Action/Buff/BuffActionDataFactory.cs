using System;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class BuffActionDataFactory : AbstractDataFactory, IBuffActionDataFactory
    {
        [Inject]
        public BuffActionDataFactory()
        {
        }

        public AbstractBuffActionData Create(AbstractBuffActionDataConfiguration configuration)
        {
            if (!BuffActionTypeValidator.TryValidate(configuration.Type))
            {
                throw new InvalidArgumentException(nameof(configuration), configuration.ToString());
            }

            AbstractBuffActionData data;
            switch (configuration.Type)
            {
                case BuffActionType.Constant:
                    if (configuration is ConstantBuffActionDataConfiguration)
                    {
                        data = new ConstantBuffActionData
                        {
                            BuffType = configuration.BuffType,
                            MaxValue = configuration.Value,
                        };

                        break;
                    }

                    throw new InvalidArgumentException(nameof(configuration),
                        configuration.ToString());
                case BuffActionType.Undefined:
                default:
                    throw new InvalidArgumentException(nameof(configuration),
                        configuration.ToString());
            }

            return data;
        }

        public bool TryCreate(AbstractBuffActionDataConfiguration configuration,
            out AbstractBuffActionData data)
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
