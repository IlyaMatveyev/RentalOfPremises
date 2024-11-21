using RentalOfPremises.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Infrastructure.Entities
{
    public  class UserEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public bool IsEmailConfirmed { get; set; } = false;
        public bool IsBanned { get; set; } = false;

        //помещения во владении пользователя
        public List<PremiseEntity>? PersonalPremises { get; set; }

        //объявления, составленные этим пользователем
        public List<AdvertEntity>? Adverts { get; set; }


        //его отклики (обявления на которые он откликнулся)
        public List<ResponseEntity>? Responses { get; set; }

        //помещения, которые арендует сам User
        public List<PremiseEntity>? RentedPremises { get; set; }
    }
}
