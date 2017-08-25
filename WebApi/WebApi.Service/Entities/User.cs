using System;

namespace WebApi.Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string HashPhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime DateRegisterLastUpdate { get; set; }

    }
}