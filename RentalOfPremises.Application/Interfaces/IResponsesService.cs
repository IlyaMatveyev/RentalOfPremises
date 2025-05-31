namespace RentalOfPremises.Application.Interfaces
{
	public interface IResponsesService
	{
		/// <summary>
		/// Создаёт отклик на объявление.
		/// </summary>
		/// <param name="advertId">Id опубликованного объявления.</param>
		/// <param name="message">Сообщение, отправленное пользователем при отклике.</param>
		/// <returns>Id созданного отклика.</returns>
		Task<Guid> Create(Guid advertId, string? message);

		/// <summary>
		/// Удаляет отклик.
		/// </summary>
		/// <param name="advertId">Id опубликованного объявления.</param>
		/// <returns>Количество удалённых записей.</returns>
		Task<int> Delete(Guid advertId);
	}
}
