using System.Drawing;

namespace Data
{
    public interface IEntity
    {
        double X { get; set; }
        double Y { get; set; }
        double MovX { get; set; }
        double MovY { get; set; }
        double Radius { get; set; }
        Color Color { get; set; }
    }
}