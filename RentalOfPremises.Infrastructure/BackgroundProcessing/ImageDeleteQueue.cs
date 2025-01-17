using RentalOfPremises.Application.Interfaces.Queues;
using RentalOfPremises.Application.Tasks;
using System.Collections.Concurrent;

namespace RentalOfPremises.Infrastructure.BackgroundProcessing
{
    public class ImageDeleteQueue : IImageDeleteQueue
    {
        private ConcurrentQueue<ImageDeleteTask> _queue = new();
        private SemaphoreSlim _semaphore = new(0);

        public void AddTask(ImageDeleteTask task)
        {
            _queue.Enqueue(task);
            _semaphore.Release();
        }

        public async Task<ImageDeleteTask> GetTask(CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync();
            _queue.TryDequeue(out var task);
            return task;
        }

    }
}
