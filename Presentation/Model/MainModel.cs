using System.Collections.ObjectModel;
using System.Windows.Threading;
using Logic;
using Prezentacja.ViewModel;

namespace Presentation.Model
{
    public class MainModel
    {
        private ObservableCollection<EntityViewModel> balls;
        public ObservableCollection<EntityViewModel> Entities => balls;
        private int width;
        private int height;
        private DispatcherTimer moveTimer;

        public MainModel()
        {
            balls = new ObservableCollection<EntityViewModel>();
            moveTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(20) };
            moveTimer.Tick += MoveBalls;
        }

        public void StartSimulation(int ballCount, int width, int height)
        {
            balls.Clear();
            this.width = width;
            this.height = height;
            SpawnBalls(ballCount);
            moveTimer.Start();
        }

        public void StopSimulation()
        {
            moveTimer.Stop();
            balls.Clear();
        }

        private void SpawnBalls(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var logic = new EntityLogic(width, height);
                var ballVM = new EntityViewModel(logic);
                balls.Add(ballVM);
            }
        }

        private void MoveBalls(object sender, EventArgs e)
        {
            foreach (var ball in balls)
            {
                ball.Logic.EntityData.Move(width, height);
                ball.Update();
            }
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            for (int i = 0; i < balls.Count; i++)
                for (int j = i + 1; j < balls.Count; j++)
                    if (balls[i].Logic.HasCollided(balls[j].Logic))
                        balls[i].Logic.ResolveCollision(balls[j].Logic);
        }
    }
}