using Microsoft.AspNetCore.Identity;

namespace WebApplication4.Models
{
    public class Uczen
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public DateTime Birthdate { get; set; }
        public string UczenUserId { get; set; }
        public IdentityUser? UczenUser { get; set; }
    }
}
