namespace Data
{
    public interface IEntity
    {
        double X { get; set; }
        double Y { get; set; }
        double MovX { get; set; }
        double MovY { get; set; }
        double Radius { get; set; }
        double Mass { get; set; }
        void Move(int maxWidth, int maxHeight);
        public event Action<double, double> EntityChanged;
        public void Start(int maxWidth, int maxHeight);
        public void Stop();
    }
}