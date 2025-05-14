using System.Collections.ObjectModel;
using System.Windows.Threading;
using Logic;
using Presentation.ViewModel;

namespace Presentation.Model
{
    public class MainModel
    {
        private ObservableCollection<EntityViewModel> balls;
        public ObservableCollection<EntityViewModel> Entities => balls;
        private List<ILogic> ballLogics = new();
        private int width;
        private int height;
        private Dispatcher dispatcher;
        private CancellationTokenSource simulationTokenSource;

        public MainModel(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            balls = new ObservableCollection<EntityViewModel>();
        }

        public void StartSimulation(int ballCount, int width, int height)
        {
            balls.Clear();
            ballLogics.Clear();
            this.width = width;
            this.height = height;
            SpawnBalls(ballCount);

            simulationTokenSource = new CancellationTokenSource();
            var token = simulationTokenSource.Token;

            foreach (var logic in ballLogics) logic.Start(width, height);
        }

        public void StopSimulation()
        {
            simulationTokenSource?.Cancel();
            foreach (var logic in ballLogics) logic.Stop();
            balls.Clear();
            ballLogics.Clear();
        }

        private void SpawnBalls(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var logic = new EntityLogic(width, height);
                var ballVM = new EntityViewModel(logic);
                balls.Add(ballVM);
                ballLogics.Add(logic);
                logic.EntityData.EntityChanged += (x, y) =>
                {
                    dispatcher.Invoke(() => ballVM.Update(x, y));
                    CheckCollisions();
                };
            }
        }

        private void CheckCollisions()
        {
            for (int i = 0; i < ballLogics.Count; i++)
                for (int j = i + 1; j < ballLogics.Count; j++)
                    if (ballLogics[i].HasCollided(ballLogics[j]))
                        ballLogics[i].ResolveCollision(ballLogics[j]);
        }
    }
}