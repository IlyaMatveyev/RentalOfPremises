using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }


        //помещения во владении пользователя
        public List<Premise>? PersonalPremises { get; set; }
        //объявления, составленные этим пользователем
        public List<Advert>? Adverts { get; set; }


        //его отклики (обявления на которые он откликнулся)
        public List<Response>? Responses { get; set; }
        //помещения, которые арендует сам User
        public List<Premise>? RentedPremises { get; set; }
    }
}
