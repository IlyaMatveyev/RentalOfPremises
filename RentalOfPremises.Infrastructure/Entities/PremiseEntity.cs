using RentalOfPremises.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Infrastructure.Entities
{
    public class PremiseEntity
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public int CoutOfRooms { get; set; } = 0;
        public double Area { get; set; } = 0;


        //владелец помещения
        public UserEntity Owner { get; set; }
        public Guid OwnerId { get; set; }

        //тот кто снимает
        public UserEntity? Renter { get; set; }
        public Guid? RenterId { get; set; }

        //объявление о сдаче этого помещения
        public AdvertEntity? Advert { get; set; }
    }
}
