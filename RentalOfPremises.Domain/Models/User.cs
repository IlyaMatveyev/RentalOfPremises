namespace RentalOfPremises.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public bool IsEmailConfirmed { get; set; } = false;
        public bool IsBanned { get; set; } = false;

        //помещения во владении пользователя
        public List<Premise>? PersonalPremises { get; set; }
        
        //объявления, составленные этим пользователем
        public List<Advert>? Adverts { get; set; }


        //его отклики (обявления на которые он откликнулся)
        public List<Response>? Responses { get; set; }
        
        //помещения, которые арендует сам User
        public List<Premise>? RentedPremises { get; set; }



        public User(string userName, string passwordHash, string email)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
        }
        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
