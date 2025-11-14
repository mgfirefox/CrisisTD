using System.Collections.Generic;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class LoadoutService : AbstractDataService<LoadoutServiceData>, ILoadoutService
    {
        private readonly IList<LoadoutItem> items = new List<LoadoutItem>();

        public IReadOnlyList<LoadoutItem> Items => items.AsReadOnly();

        public int Count { get; private set; }

        [Inject]
        public LoadoutService(Scene scene) : base(scene)
        {
        }

        public LoadoutItem GetItem(int index)
        {
            return items[index];
        }

        protected override void OnInitialized(LoadoutServiceData data)
        {
            base.OnInitialized(data);

            InitializeItems(data.Items);
        }

        private void InitializeItems(IList<LoadoutItem> items)
        {
            foreach (LoadoutItem item in items)
            {
                this.items.Add(item.Clone() as LoadoutItem);
            }

            Count = this.items.Count;
        }

        protected override void OnDestroying()
        {
            ClearItems();

            base.OnDestroying();
        }

        private void ClearItems()
        {
            items.Clear();
        }
    }
}
