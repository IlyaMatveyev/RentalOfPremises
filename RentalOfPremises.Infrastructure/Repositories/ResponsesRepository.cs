using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Auth;
using RentalOfPremises.Infrastructure.Entities;
using RentalOfPremises.Infrastructure.MSSQLServer;

namespace RentalOfPremises.Infrastructure.Repositories
{
	public class ResponsesRepository : IResponsesRepository
	{
		private readonly IRentalOfPremisesDbContext _dbContext;
		private readonly ICurrentUserContext _currentUserContext;

		public ResponsesRepository(
			IRentalOfPremisesDbContext dbContext, 
			ICurrentUserContext currentUserContext)
		{
			_dbContext = dbContext;
			_currentUserContext = currentUserContext;
		}

		public async Task<Guid> Add(Guid advertId, string? message)
		{
			var senderId = _currentUserContext.UserId;

			// Находим объявление с флагом откликался ли этот пользователь на это объявление.
			var advertWithResponse = await _dbContext.Adverts
				.AsNoTracking()
				.Where(a => a.Id == advertId && a.IsPublished == true)
				.Select(a =>
					new
					{
						AdvertEntity = a,	// Само объявление
						AlreadyResponded = _dbContext.Responses.Any(r => r.SenderId == senderId && r.AdvertId == advertId)	// Флаг
					})
				.FirstOrDefaultAsync();

			// Объявление не найдено или не опубликовано.
			if (advertWithResponse is null)
			{
				throw new KeyNotFoundException("Advert not found or not published.");
			}

			// Пользователь попытался откликнуться на своё объявление.
			if (advertWithResponse.AdvertEntity.OwnerId == senderId)
			{
				throw new InvalidOperationException("You cannot respond to your own advert.");
			}

			// Пользователь уже откликался на это объявление.
			if (advertWithResponse.AlreadyResponded)
			{
				throw new InvalidOperationException("You already responded to this advert.");
			}

			var newResponse = new ResponseEntity
			{
				Id = Guid.NewGuid(),
				Message = message,
				SenderId = senderId,
				AdvertId = advertId
			};

			_dbContext.Responses.Add(newResponse);
			await _dbContext.SaveChangesAsync();

			return newResponse.Id;
		}

		public async Task<int> Delete(Guid advertId)
		{
			var senderId = _currentUserContext.UserId;

			var countOfDeletedRows = await _dbContext.Responses
				.Where(a => a.SenderId == senderId && a.AdvertId == advertId)
				.ExecuteDeleteAsync();

			return countOfDeletedRows;
		}
	}
}
