using Data;

namespace Logic
{
    public class EntityLogic : ILogic
    {
        public IEntity EntityData { get; set; }

        public EntityLogic(int width, int height)
        {
            EntityData = Ball.CreateBall(width, height);
        }

        public EntityLogic(IEntity entityData)
        {
            EntityData = entityData;
        }

        public bool HasCollided(ILogic other)
        {
            double dx = EntityData.X - other.EntityData.X;
            double dy = EntityData.Y - other.EntityData.Y;
            double distanceSq = dx * dx + dy * dy;
            double radiusSum = (EntityData.Radius + other.EntityData.Radius) / 2;
            double minDistanceSq= (radiusSum * 1.2) * (radiusSum * 1.1);

            return distanceSq <= minDistanceSq;
        }

        public void ResolveCollision(ILogic other)
        {
            double x1 = EntityData.X;
            double y1 = EntityData.Y;
            double x2 = other.EntityData.X;
            double y2 = other.EntityData.Y;
            double m1 = EntityData.Mass;

            double vx1 = EntityData.MovX;
            double vy1 = EntityData.MovY;
            double vx2 = other.EntityData.MovX;
            double vy2 = other.EntityData.MovY;
            double m2 = other.EntityData.Mass;

            double dx = x1 - x2;
            double dy = y1 - y2;

            double distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance == 0) return;

            double nx = dx / distance;
            double ny = dy / distance;

            double dvx = vx1 - vx2;
            double dvy = vy1 - vy2;

            double velocityAlongNormal = dvx * nx + dvy * ny;
            if (velocityAlongNormal > 0) return;

            double impulse = -2 * velocityAlongNormal;
            impulse /= (1 / m1 + 1 / m2);

            double impulseX = impulse * nx;
            double impulseY = impulse * ny;

            EntityData.MovX += impulseX / m1;
            EntityData.MovY += impulseY / m1;
            other.EntityData.MovX -= impulseX / m2;
            other.EntityData.MovY -= impulseY / m2;
        }

    }
}