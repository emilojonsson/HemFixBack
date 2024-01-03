using HemFixBack.Models;

namespace HemFixBack.Services
{
    public interface IUserService
    {
        public User Get(UserLogin userLogin);
    }
}
