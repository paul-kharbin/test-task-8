using System.Threading.Tasks;
using TestTask.Infrastructure.Contact.Abstraction;
using TestTask.Infrastructure.Contact.Model;

namespace TestTask.Infrastructure.Contact.Services;

public interface IUsersService : IService<User>
{
    Task AddAsync(User user);
    Task<bool> DeleteAsync(User user);
    Task<User?> FindByIdAsync(int id);
    Task<User?> FindByLoginAsync(string normalizedLogin);
    Task<bool> UpdateAsync(User user);
}
