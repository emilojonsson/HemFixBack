using HemFixBack.Models;

namespace HemFixBack.Repositories
{
    public class UserRepository
    {
        public static List<User> Users = new()
        {
            new() { Username = Environment.GetEnvironmentVariable("userName1"), EmailAddress = "emil.o.jonsson@gmail.com", Password = Environment.GetEnvironmentVariable("password1"), GivenName = "Emil", Surname = "Jönsson", Role = "Administrator" },
            new() { Username = Environment.GetEnvironmentVariable("userName2"), EmailAddress = "tia.sundin@gmail.com", Password = Environment.GetEnvironmentVariable("password2"), GivenName = "Patricia", Surname = "Sundin", Role = "Standard" },
        };
    }
}
