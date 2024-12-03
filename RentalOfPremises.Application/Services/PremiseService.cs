using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Services
{
    public class PremiseService : IPremiseService
    {
        private readonly IPremiseRepository _premiseRepository;
        public PremiseService(IPremiseRepository premiseRepository)
        {
            _premiseRepository = premiseRepository;
        }

        public async Task<Guid> Add(Premise premise, Guid userId)
        {
            premise.OwnerId = userId;

            return await _premiseRepository.Add(premise);
        }

        public async Task<Premise?> GetById(Guid premisId, Guid userId)
        {
            //TODO: Не забыть проверить что пользователь владелец этой записи
            var premise = await _premiseRepository.ReadById(premisId);
            if (premise == null || premise.OwnerId != userId)
            {
                return null;
            }
            return premise;
        }

        public async Task<List<Premise>?> GetAll(Guid userId)
        {
            return await _premiseRepository.ReadAll(userId);
        }

        public async Task<int> Delete(Guid premisId, Guid userId)
        {
            //TODO: Тут нужно будет вытащить Premis по id и проверить OwnerId с userId
            var countOfDelitedObjects = await _premiseRepository.Delete(premisId, userId);

            return countOfDelitedObjects;
        }

        public async Task<Guid> Update(Guid premisId, Premise premises, Guid userId)
        {
            return await _premiseRepository.Update(premisId, premises, userId);
        }
    }
}
