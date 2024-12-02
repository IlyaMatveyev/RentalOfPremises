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

        public async Task<Premise> GetById(Guid premisId, Guid userId)
        {
            //TODO: Не забыть проверить что пользователь владелец этой записи
            throw new NotImplementedException();
        }

        public async Task<List<Premise>> GetAll(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid premisId, Guid userId)
        {
            //TODO: Тут нужно будет вытащить Premis по id и проверить OwnerId с userId
            throw new NotImplementedException();
        }

        public async Task<Guid> Update(Guid premisId, Premise premise, Guid userId)
        {
            //TODO: Тут нужно будет вытащить Premis по id и проверить OwnerId с userId
            throw new NotImplementedException();
        }
    }
}
