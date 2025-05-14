using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Logic;

namespace Prezentacja.ViewModel
{
    public class EntityViewModel : INotifyPropertyChanged
    {
        private EntityLogic ball;
        private Random random = new Random();
        public double X => ball.EntityData.X;
        public double Y => ball.EntityData.Y;
        public double Radius => ball.EntityData.Radius;
        public Brush BallColor => new SolidColorBrush(Color.FromArgb(255,
                (byte)random.Next(0, 230), (byte)random.Next(0, 230), (byte)random.Next(0, 230)));

        public EntityViewModel(EntityLogic ball)
        {
            this.ball = ball;
        }

        public void Update()
        {
            OnPropertyChanged(nameof(X));
            OnPropertyChanged(nameof(Y));
        }

        public EntityLogic Logic => ball;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}