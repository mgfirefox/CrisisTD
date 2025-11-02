namespace Mgfirefox.CrisisTd
{
    public interface ILoadoutService : IDataService<LoadoutServiceData>, ILoadoutModel
    {
        int Count { get; }

        LoadoutItem GetItem(int index);
    }
}
