using RentalOfPremises.Application.Interfaces.Queues;
using RentalOfPremises.Application.Tasks;
using System.Collections.Concurrent;

namespace RentalOfPremises.Infrastructure.BackgroundProcessing
{
    public class ImageUploadQueue : IImageUploadQueue
    {
        private readonly ConcurrentQueue<ImageUploadTask> _queue = new();
        private readonly SemaphoreSlim _semaphore = new(0);

        public void AddTask(ImageUploadTask task)
        {
            _queue.Enqueue(task);   //добавление таски в конц очереди
            _semaphore.Release();   //увеличивает счётчик потоков в семафоре на 1
        }

        public async Task<ImageUploadTask> GetTaskAsync(CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync();       //Ждём задачу. (если счётчик потоков = 0, ждём освобождения. Если больше, то предоставляем доступ)
            _queue.TryDequeue(out var task);    //Извлекаем задачу из очереди
            return task;
        }
    }

}
