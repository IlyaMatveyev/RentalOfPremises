using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Domain.Models
{
    //Объявление о сдаче помещения
    public class Advert 
    {
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<Response>? Responses { get; set; } //список откликов

        //владелец
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }

        //помещение
        public Guid PremiseId { get; set; }
        public Premise Premise { get; set; }
    }
}
