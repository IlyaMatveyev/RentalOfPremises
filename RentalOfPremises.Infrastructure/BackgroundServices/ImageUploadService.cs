using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Queues;

namespace RentalOfPremises.Infrastructure.BackgroundServices
{
    public class ImageUploadService : BackgroundService
    {
        private const int MAX_TASKS_COUNT = 10;
        private SemaphoreSlim _semaphore = new(MAX_TASKS_COUNT);

        private readonly IImageUploadQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        public ImageUploadService(
            IImageUploadQueue queue,
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
                var task = await _queue.GetTaskAsync(stoppingToken);

                //ограничиваем кол-во одновременно выполняющихся задач
                await _semaphore.WaitAsync(stoppingToken);

                var runningTask = Task.Run(async () =>
                {
                    try
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            //создаём Scope сервисы в контексте каждой отдельной задачи, т.к. background сервис - Singleton
                            var imagesInAdvertRepository = scope.ServiceProvider.GetRequiredService<IImagesInAdvertRepository>();
                            var imageStorage = scope.ServiceProvider.GetRequiredService<IImageStorage>();

                            var url = await imageStorage.UploadFileBytes(task.FileBytes, task.PathInCloud);
                            await imagesInAdvertRepository.Add(task.AdvertId, url);
                        }
                    }
                    catch(Exception ex)
                    {
                        //TODO: Придумать как сюда впихнуть логгирование
                    }
                    finally
                    {
                        //Освобождаем место для следующей задачи
                        _semaphore.Release();
                    }

                }, stoppingToken);


                runningTaskList.Add(runningTask);
                runningTaskList.RemoveAll(t => t.IsCompleted ||  t.IsFaulted || t.IsCanceled);

            }

            await Task.WhenAll(runningTaskList);
        }
    }
}
