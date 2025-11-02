using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class BaseService : AbstractService, IBaseService
    {
        private readonly IBaseFactory factory;

        private readonly IList<IBaseView> bases = new List<IBaseView>();
        private readonly IDictionary<IBaseView, Action> baseDestroyingActions =
            new Dictionary<IBaseView, Action>();

        public IReadOnlyCollection<IBaseView> Bases => bases.AsReadOnly();
        public int Count => bases.Count;

        [Inject]
        public BaseService(IBaseFactory factory)
        {
            this.factory = factory;
        }

        public IBaseView Spawn()
        {
            try
            {
                IBaseView @base = factory.Create();

                Action baseDestroyingAction = () => OnBaseDestroying(@base);

                @base.Destroying += baseDestroyingAction;

                bases.Add(@base);
                baseDestroyingActions[@base] = baseDestroyingAction;

                return @base;
            }
            catch (Exception e)
            {
                if (e is not (InvalidArgumentException or ConfigurationNotFoundException))
                {
                    throw new CaughtUnexpectedCheckedException(e);
                }

                throw new SpawnFailedException("Base", e);
            }
        }

        public bool TrySpawn(out IBaseView view)
        {
            try
            {
                view = Spawn();

                return true;
            }
            catch (Exception e)
            {
                if (e is not (InvalidArgumentException or PrefabNotFoundException))
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            view = null;

            return false;
        }

        public IBaseView Get(int index)
        {
            if (index < 0 || index >= bases.Count)
            {
                throw new InvalidArgumentException(nameof(index), index.ToString());
            }

            return bases[index];
        }

        public void DespawnAll()
        {
            foreach (IBaseView @base in bases)
            {
                @base.Destroying -= baseDestroyingActions[@base];

                @base.Destroy();
            }
            bases.Clear();
            baseDestroyingActions.Clear();
        }

        private void OnBaseDestroying(IBaseView @base)
        {
            @base.Destroying -= baseDestroyingActions[@base];

            bases.Remove(@base);
            baseDestroyingActions.Remove(@base);
        }
    }
}
