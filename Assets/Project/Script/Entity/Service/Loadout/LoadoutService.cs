using System.Collections.Generic;
using Unity.VisualScripting;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class LoadoutService : AbstractDataService<LoadoutServiceData>, ILoadoutService
    {
        private readonly IList<LoadoutItem> loadout = new List<LoadoutItem>();

        public IReadOnlyList<LoadoutItem> Loadout => loadout.AsReadOnly();

        public int Count { get; private set; }

        [Inject]
        public LoadoutService(Scene scene) : base(scene)
        {
        }

        public LoadoutItem GetItem(int index)
        {
            return loadout[index];
        }

        protected override void OnInitialized(LoadoutServiceData data)
        {
            base.OnInitialized(data);

            loadout.Clear();
            loadout.AddRange(data.Loadout);

            Count = loadout.Count;
            for (int i = 0; i < loadout.Count; i++)
            {
                LoadoutItem loadoutItem = loadout[i];

                if (loadoutItem.TowerId == TowerId.Undefined)
                {
                    Count = i;

                    break;
                }
            }
        }
    }
}
