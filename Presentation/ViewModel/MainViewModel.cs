using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Presentation.Model;
using System.Windows;
using Presentation.Commands;
using Prezentacja.ViewModel;

namespace Presentation.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainModel model;
        private int ballCount;
        public int BallCount
        {
            get { return ballCount; }
            set
            {
                ballCount = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EntityViewModel> AllBalls => model.Entities;

        public ICommand StartCommand { get; }

        public MainViewModel()
        {
            model = new MainModel();
            ballCount = 10;
            StartCommand = new Command(StartSimulation);
        }

        private void StartSimulation(object parameter)
        {
            if (parameter is FrameworkElement element)
                model.StartSimulation(ballCount, (int)element.ActualWidth, (int)element.ActualHeight);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}