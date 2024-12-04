﻿using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.DTOs
{
    public class PremiseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty; //название, которое будет видеть только владелец. (Для удобства)
        public string Address { get; set; } = string.Empty;
        public int CoutOfRooms { get; set; } = 0;
        public double Area { get; set; } = 0;


        //владелец помещения
        public string OwnerName { get; set; } = string.Empty;

        //тот кто снимает
        public string? RenterName { get; set; } = string.Empty;

    }
}