using RentalOfPremises.Application.Tasks;

namespace RentalOfPremises.Application.Interfaces.Queues
{
    public interface IFolderDeleteQueue
    {
        void AddTask(FolderDeleteTask task);
        Task<FolderDeleteTask> GetTask(CancellationToken cancellationToken);
    }
}
