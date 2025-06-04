using System.Collections.Concurrent;
using System.Diagnostics;

namespace Data
{
    public class Log
    {
        private static Log instance;
        private static readonly object _syncLock = new object();

        private ConcurrentQueue<string> logQueue;
        private CancellationTokenSource cancellationTokenSource;
        private Task loggingTask;

        private Log()
        {
            logQueue = new ConcurrentQueue<string>();
        }

        public static Log Instance
        {
            get
            {
                if (instance == null)
                    lock (_syncLock)
                        if (instance == null) instance = new Log();
                return instance;
            }
        }

        public void StartTask()
        {
            if (loggingTask != null && !loggingTask.IsCompleted) StopTask();

            string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"balls_{DateTime.Now:ddMMyy_HHmmss}.log");
            Debug.WriteLine($"Logging to {logFile}!");

            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            loggingTask = Task.Run(async () =>
            {
                using (StreamWriter writer = new StreamWriter(logFile, append: true))
                {
                    try
                    {
                        while (!token.IsCancellationRequested || !logQueue.IsEmpty)
                        {
                            if (logQueue.TryDequeue(out string logEntry))
                                await writer.WriteLineAsync($"{DateTime.Now:HH:mm:ss.fff} – {logEntry}");
                            else await Task.Delay(100, token);
                        }
                    }
                    catch (Exception ignored)
                    {
                    }
                }
            }, token);
        }

        public void StopTask()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                try
                {
                    loggingTask?.Wait();
                }
                catch (AggregateException ex)
                {
                    ex.Handle(e => e is TaskCanceledException);
                }
                finally
                {
                    cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                    loggingTask = null;
                }
            }
        }

        public void LogMessage(string message)
        {
            logQueue.Enqueue(message);
        }
    }
}
