using RentalOfPremises.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Infrastructure.Entities
{
    public class ResponseEntity
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;

        //тот кто откликнулся
        public UserEntity? Sender { get; set; }
        public Guid? SenderId { get; set; }

        //объявление
        public AdvertEntity Advert { get; set; }
        public Guid AdvertId { get; set; }
    }
}
