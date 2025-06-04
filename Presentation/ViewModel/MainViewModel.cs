using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Presentation.Model;
using System.Windows;
using Presentation.Commands;
using System.Windows.Threading;

namespace Presentation.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainModel model;
        private int ballCount = 10;
        public int BallCount
        {
            get { return ballCount; }
            set
            {
                ballCount = (value > 100 ? 100 : value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EntityViewModel> AllBalls => model.Entities;

        public ICommand StartCommand { get; }
        public ICommand BreakBall { get; }

        public MainViewModel()
        {
            model = new MainModel(Dispatcher.CurrentDispatcher);
            StartCommand = new Command(StartSimulation);
            BreakBall = new Command(PerformBreakBall);
        }

        private void StartSimulation(object parameter)
        {
            if (parameter is FrameworkElement element)
                model.StartSimulation(ballCount, (int)element.ActualWidth, (int)element.ActualHeight);
        }

        public void PerformBreakBall(object parameter)
        {
            model.BreakBall();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}