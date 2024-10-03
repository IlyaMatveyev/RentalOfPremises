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
        public string Message { get; set; } = string.Empty;

        //тот кто откликнулся
        public User? Sender { get; set; }
        public Guid? SenderId { get; set; }

        //объявление
        public Advert Advert { get; set; }
        public Guid AdvertId { get; set; }
    }
}
