using RentalOfPremises.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Infrastructure.Entities
{
    public class AdvertEntity
    {
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<ResponseEntity>? Responses { get; set; } //список откликов

        //владелец
        public Guid OwnerId { get; set; }
        public UserEntity Owner { get; set; }

        //помещение
        public Guid PremiseId { get; set; }
        public PremiseEntity Premise { get; set; }
    }
}
