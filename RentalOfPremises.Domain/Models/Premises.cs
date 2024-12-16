namespace RentalOfPremises.Domain.Models
{
    //Класс помещения
    public class Premises
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty; //название, которое будет видеть только владелец. (Для удобства)
        public string Address { get; set; } = string.Empty;
        public int CoutOfRooms { get; set; } = 0;
        public double Area { get; set; } = 0;
        public string MainImageUrl { get; set; } = string.Empty;

        //владелец помещения
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }

        //тот кто снимает
        public User? Renter { get; set; }
        public Guid? RenterId { get; set; }

        //объявление о сдаче этого помещения
        public Advert? Advert { get; set; }

        public Premises()
        {
            Id = Guid.NewGuid();
        }

    }
}
