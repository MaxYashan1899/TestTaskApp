using TestTaskApp.Models;

namespace TestTaskApp.Data
{
    public interface IUserRepository
    {
        User Create(User user, string login);

        User GetByLogin(string login);

        User GetById(int id);

        bool ExistsByLogin(string login);
    }
}
