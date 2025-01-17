using RentalOfPremises.Application.Tasks;

namespace RentalOfPremises.Application.Interfaces.Queues
{
    public interface IImageDeleteQueue
    {
        void AddTask(ImageDeleteTask task);
        Task<ImageDeleteTask> GetTask(CancellationToken cancellationToken);
    }
}
