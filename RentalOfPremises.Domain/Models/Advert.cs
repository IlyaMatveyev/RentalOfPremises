namespace RentalOfPremises.Domain.Models
{
    //Объявление о сдаче помещения
    public class Advert 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Label { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;

        public bool IsPublished { get; set; } = false;

        //главное фото объявления
        public string MainImageUrl = string.Empty;

        //список фото в объявлении
        public List<ImageInAdvert>? ListImageUrl {  get; set; }

        public List<Response>? Responses { get; set; } //список откликов

        //владелец
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }

        //помещение
        public Guid PremiseId { get; set; }
        public Premises Premise { get; set; }
    }
}
