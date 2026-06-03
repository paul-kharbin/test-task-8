using TestTask.Data.Contract.Abstration;
using TestTask.Data.Contract.Model;

using System.Threading.Tasks;

namespace TestTask.Data.Contract.Rrepository;

public interface IUsersRepository : IRepository<DataUser>
{
    Task<bool> DeleteAsync(DataUser user);
    Task<DataUser?> FindByIdAsync(int id);
    Task<DataUser?> FindByLoginAsync(string login);
    Task<bool> UpdateAsync(DataUser user);
}
