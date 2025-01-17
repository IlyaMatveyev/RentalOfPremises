using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Queues;

namespace RentalOfPremises.Infrastructure.BackgroundServices
{
    public class ImageDeleteService : BackgroundService
    {
        private readonly IImageDeleteQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;

        private const int MAX_TASKS_COUNT = 10;
        private readonly SemaphoreSlim _semaphore = new(MAX_TASKS_COUNT);

        public ImageDeleteService(
            IImageDeleteQueue queue, 
            IServiceScopeFactory scopeFactory
            )
        {
            _queue = queue;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var runningTaskList = new List<Task>();

            while (!stoppingToken.IsCancellationRequested)
            {
                var task = await _queue.GetTask(stoppingToken);
                await _semaphore.WaitAsync();


                var runningTask = Task.Run(async () =>
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var imagesInAdvertRepository = scope.ServiceProvider.GetRequiredService<IImagesInAdvertRepository>();
                        var imageStorage = scope.ServiceProvider.GetRequiredService<IImageStorage>();

                        var countOfDeleted = await imagesInAdvertRepository.Delete(task.AdvertId, task.ImageUrl);
                        if(countOfDeleted > 0)
                        {
                            await imageStorage.DeleteImageByUrl(task.ImageUrl, task.UserId);
                        }
                    }

                    _semaphore.Release();
                }, 
                stoppingToken);
                
                runningTaskList.Add(runningTask);
                runningTaskList.RemoveAll(t => t.IsCanceled || t.IsCompleted || t.IsFaulted);

            }

            await Task.WhenAll(runningTaskList);
        }
    }
}
