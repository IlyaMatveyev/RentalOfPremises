﻿using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Infrastructure.Entities
{
    public class AdvertEntity
    {
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public bool IsPublished { get; set; } = false;

        //главное фото объявления
        public string MainImageUrl = string.Empty;
        //список фото в объявлении
        public List<ImageInAdvertEntity>? ListImageUrl { get; set; }

        public List<ResponseEntity>? Responses { get; set; } //список откликов

        //владелец
        public Guid OwnerId { get; set; }
        public UserEntity Owner { get; set; }

        //помещение
        public Guid PremiseId { get; set; }
        public PremiseEntity Premise { get; set; }
    }
}
