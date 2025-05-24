using RentalOfPremises.Application.Interfaces.Queues;
using RentalOfPremises.Application.Tasks;
using System.Collections.Concurrent;

namespace RentalOfPremises.Infrastructure.BackgroundProcessing
{
    public class FolderDeleteQueue : IFolderDeleteQueue
    {
        private readonly ConcurrentQueue<FolderDeleteTask> _queue = new();
        private readonly SemaphoreSlim _semaphore = new(0);

        public void AddTask(FolderDeleteTask task)
        {
            _queue.Enqueue(task);
            _semaphore.Release();
        }

        public async Task<FolderDeleteTask> GetTask(CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync(cancellationToken);
            _queue.TryDequeue(out var task);
            return task;
        }
    }
}
