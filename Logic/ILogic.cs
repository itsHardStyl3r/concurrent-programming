using Data;

namespace Logic
{
    public interface ILogic
    {
        IEntity EntityData { get; }
        bool HasCollided(ILogic other);
        void ResolveCollision(ILogic other);
    }
}
