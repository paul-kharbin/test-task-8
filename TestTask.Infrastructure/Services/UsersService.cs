using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestTask.Data.Contract.Model;
using TestTask.Data.Contract.Rrepository;
using TestTask.Infrastructure.Contact.Model;
using TestTask.Infrastructure.Contact.Services;

namespace TestTask.Infrastructure.Services;

internal sealed class UsersService(IUsersRepository repository, IMapper mapper) : IUsersService
{
    #region IUsersService Implementaiton

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        var users = await repository.GetAllAsync();

        return users.Select(mapper.Map<User>).ToList();
    }

    public async Task AddAsync(User user)
    {
        await repository.AddAsync(mapper.Map<DataUser>(user));
    }

    public async Task<bool> UpdateAsync(User user)
    {
        return await repository.UpdateAsync(mapper.Map<DataUser>(user));
    }

    public async Task<bool> DeleteAsync(User user)
    {
        return await repository.DeleteAsync(mapper.Map<DataUser>(user));
    }

    public async Task<User?> FindByIdAsync(int id)
    {
        var user = await repository.FindByIdAsync(id);

        return user is null ? null : mapper.Map<User>(user);
    }

    public async Task<User?> FindByLoginAsync(string normalizedLogin)
    {
        var user = await repository.FindByLoginAsync(normalizedLogin);

        return user is null ? null : mapper.Map<User>(user);
    }

    #endregion
}
