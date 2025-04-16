using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;

namespace Prezentacja.ViewModel
{
    public class EntityViewModel : INotifyPropertyChanged
    {
        private EntityLogic ball;

        public double X => ball.EntityData.X;
        public double Y => ball.EntityData.Y;
        public double Radius => ball.EntityData.Radius;

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