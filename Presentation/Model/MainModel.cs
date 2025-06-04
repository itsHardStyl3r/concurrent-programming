using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Threading;
using Data;
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
        private readonly object _syncLock = new();

        private DispatcherTimer diagTimer;

        public MainModel(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            balls = new ObservableCollection<EntityViewModel>();

            diagTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
            diagTimer.Tick += DiagnosticLog;
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

            Log.Instance.StartTask();
            Log.Instance.LogMessage("Simulation " + width + "x" + height + " started with " + ballCount + " balls.");

            diagTimer.Start();
            Log.Instance.LogMessage("-- Diag timer started");
        }

        public void BreakBall()
        {
            if (balls.Count == 0) return;
            Debug.Print("Breaking last ball...");
            int lastBall = ballLogics.Count;
            ballLogics[lastBall - 1].Stop();
            ballLogics.RemoveAt(lastBall - 1);
            balls.RemoveAt(lastBall - 1);
        }

        public void StopSimulation()
        {
            simulationTokenSource?.Cancel();
            foreach (var logic in ballLogics) logic.Stop();
            balls.Clear();
            ballLogics.Clear();
            Log.Instance.StopTask();
            diagTimer.Stop();
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
            lock (_syncLock)
            {
                for (int i = 0; i < ballLogics.Count; i++)
                    for (int j = i + 1; j < ballLogics.Count; j++)
                        if (ballLogics[i].HasCollided(ballLogics[j]))
                            ballLogics[i].ResolveCollision(ballLogics[j]);
            }
        }

        private void DiagnosticLog(object sender, EventArgs e)
        {
            lock (_syncLock)
            {
                for (int i = 0; i < ballLogics.Count; i++)
                {
                    var logic = ballLogics[i];
                    Log.Instance.LogMessage($"Ball {i}: Position=({logic.EntityData.X}, {logic.EntityData.Y}), Velocity=({logic.EntityData.MovX}, {logic.EntityData.MovY}), Radius={logic.EntityData.Radius}");
                }
            }
        }
    }
}