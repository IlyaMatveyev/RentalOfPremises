using RentalOfPremises.Application.Tasks;

namespace RentalOfPremises.Application.Interfaces.Queues
{
    public interface IImageUploadQueue
    {
        void AddTask(ImageUploadTask task);
        Task<ImageUploadTask> GetTaskAsync(CancellationToken cancellationToken);
    }
}
