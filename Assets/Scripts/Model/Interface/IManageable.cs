namespace Model
{
    public interface IManageable
    {
        bool IsEnabled { get; }
        void Init(ObjectInitialData data);
        void UpdateObject(float dt);
        void Retain();
        void Destroy();
    }
}
