using System;
using System.Threading;
using System.Threading.Tasks;

namespace Konsole
{
    public class BackgroundJob<TResult>
    {
        private readonly Func<TResult> job;
        private readonly IProgressIndicator progressIndicator;

        public BackgroundJob(Func<TResult> job, IProgressIndicator progressIndicator)
        {
            this.job = job;
            this.progressIndicator = progressIndicator;
        }

        public TResult Run()
        {
            var task = Task.Run(job);
            progressIndicator.Start();
            while (!task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                Thread.Sleep(progressIndicator.UpdateInterval);
                progressIndicator.Update();
            }
            progressIndicator.Finish();
            if (task.IsCompleted)
            {
                return task.Result;
            }
            if (task.IsFaulted)
            {
                throw task.Exception.Flatten();
            }
            throw new TaskCanceledException();
        }
    }
}