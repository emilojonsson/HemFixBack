using HemFixBack.Models;

namespace HemFixBack
{
    public interface IUserService
    {
        public User Get(UserLogin userLogin);
    }
}
