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

        private double CalculateDistance(double dx, double dy)
        {
            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }

        public bool HasCollided(ILogic other)
        {
            double dx = (EntityData.X + EntityData.Radius / 2) - (other.EntityData.X + other.EntityData.Radius / 2);
            double dy = (EntityData.Y + EntityData.Radius / 2) - (other.EntityData.Y + other.EntityData.Radius / 2);
            double distance = CalculateDistance(dx, dy);
            double minDistance = (EntityData.Radius / 2 + other.EntityData.Radius / 2) * 1.1;
            return distance <= minDistance;
        }

        public void ResolveCollision(ILogic other)
        {
            double dx = (EntityData.X + EntityData.Radius / 2) - (other.EntityData.X + other.EntityData.Radius / 2);
            double dy = (EntityData.Y + EntityData.Radius / 2) - (other.EntityData.Y + other.EntityData.Radius / 2);
            double distance = CalculateDistance(dx, dy);

            double minDistance = (EntityData.Radius / 2 + other.EntityData.Radius / 2) * 1.1;
            if (distance < minDistance)
            {
                double overlap = minDistance - distance;
                double knockX = dx / distance * (overlap / 2);
                double knockY = dy / distance * (overlap / 2);

                EntityData.X += knockX;
                EntityData.Y += knockY;
                other.EntityData.X -= knockX;
                other.EntityData.Y -= knockY;
            }
            (EntityData.MovX, EntityData.MovY) = (other.EntityData.MovX, other.EntityData.MovY);
        }
    }
}