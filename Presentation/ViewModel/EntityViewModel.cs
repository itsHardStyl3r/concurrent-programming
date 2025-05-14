using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Logic;

namespace Presentation.ViewModel
{
    public class EntityViewModel : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _radius;
        private Random random = new Random();

        public double X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(); }
        }

        public double Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(); }
        }

        public double Radius
        {
            get => _radius;
            set { _radius = value; OnPropertyChanged(); }
        }

        public Brush BallColor => new SolidColorBrush(Color.FromArgb(255,
                (byte)random.Next(0, 230), (byte)random.Next(0, 230), (byte)random.Next(0, 230)));

        public EntityViewModel(ILogic logic)
        {
            if (logic == null) return;
            X = logic.EntityData.X;
            Y = logic.EntityData.Y;
            Radius = logic.EntityData.Radius;
        }

        public void Update(double x, double y)
        {
            X = x;
            Y = y;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}