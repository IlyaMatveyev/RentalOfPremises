namespace RentalOfPremises.Application.DTOs.AdvertResponseDto
{
	/// <summary>
	/// Dto для запроса на создание отклика(Response) на объявление.
	/// </summary>
	public class AdvertResponseCreateRequest
	{
		/// <summary>
		/// Id опубликованного объявления.
		/// </summary>
		public Guid PublishedAdvertId { get; set; } = Guid.Empty;
		
		/// <summary>
		/// Сообщение к отклику.
		/// </summary>
		public string? Message {  get; set; } = null;
	}
}
