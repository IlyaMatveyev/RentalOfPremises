using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Domain.Models
{
    //Класс помещения
    public class Premise
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public int CoutOfRooms { get; set; } = 0;
        public double Area { get; set; } = 0;

        //владелец помещения
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }

        //тот кто снимает
        public User Renter { get; set; }
        public Guid RenterId { get; set; }

        //объявление о сдаче этого помещения
        public Advert? Advert { get; set; }
        public Guid AdvertId { get; set; }
    }
}
