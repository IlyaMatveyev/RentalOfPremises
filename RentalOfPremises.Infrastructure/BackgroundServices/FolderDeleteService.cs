using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Queues;

namespace RentalOfPremises.Infrastructure.BackgroundServices
{
    public class FolderDeleteService : BackgroundService
    {
        private readonly IFolderDeleteQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;

        private const int MAX_TASKS_COUNT = 10;
        private readonly SemaphoreSlim _semaphore = new(MAX_TASKS_COUNT);

        public FolderDeleteService(
            IFolderDeleteQueue queue, 
            IServiceScopeFactory scopeFactory)
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
                await _semaphore.WaitAsync(stoppingToken);

                var runningTask = Task.Run(async() => 
                {
                    using (var scope = _scopeFactory.CreateScope()){
                        var imageStorage = scope.ServiceProvider.GetRequiredService<IImageStorage>();

                        var isFolderDeleted = await imageStorage.DeleteFolder(task.Path);

                        if (!isFolderDeleted)
                        {
                            _queue.AddTask(task);
                        }
                    }

                    _semaphore.Release();

                }, stoppingToken);

                runningTaskList.Add(runningTask);
                runningTaskList.RemoveAll(t => t.IsCompleted || t.IsCanceled || t.IsFaulted);
            }

            await Task.WhenAll(runningTaskList);
        }
    }
}
