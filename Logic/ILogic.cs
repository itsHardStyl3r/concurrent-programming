using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
