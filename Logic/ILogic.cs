using Data;

namespace Logic
{
    public interface ILogic
    {
        IEntity EntityData { get; }
        void Move(int maxWidth, int maxHeight);
        bool HasCollided(ILogic other);
        void ResolveCollision(ILogic other);
    }
}
