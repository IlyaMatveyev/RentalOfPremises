using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Domain.Models
{
    public class Response
    {
        public Guid Id { get; set; }
        public string Message { get; set; }

        //тот кто откликнулся
        public User User { get; set; }
        public Guid UserId { get; set; }

        //объявление
        public Advert Advert { get; set; }
        public Guid AdvertId { get; set; }
    }
}
