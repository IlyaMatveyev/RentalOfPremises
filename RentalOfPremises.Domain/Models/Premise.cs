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
        public string Address { get; set; }
        public int CoutOfRooms { get; set; }
        public double Area { get; set; }

        //владелец помещения
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }


        //объявление о сдаче этого помещения
        public Advert? Advert { get; set; }
        public Guid AdvertId { get; set; }
    }
}
