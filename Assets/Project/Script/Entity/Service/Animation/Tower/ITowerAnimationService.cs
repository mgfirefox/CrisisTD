namespace Mgfirefox.CrisisTd
{
    public interface ITowerAnimationService : IDataService<TowerAnimationServiceData>
    {
        void SetBool(string name, bool value);
        void SetFloat(string name, float value);
        void SetInt(string name, int value);
        void ActivateTrigger(string name);
    }
}
