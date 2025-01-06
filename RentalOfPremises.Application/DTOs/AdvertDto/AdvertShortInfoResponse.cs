namespace RentalOfPremises.Application.DTOs.AdvertDto
{
    public class AdvertShortInfoResponse
    {
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;


        public string Address { get; set; } = string.Empty;
        public int CoutOfRooms { get; set; } = 0;
        public double Area { get; set; } = 0;



        //главное фото объявления
        public string MainImageUrl = string.Empty;
    }
}
