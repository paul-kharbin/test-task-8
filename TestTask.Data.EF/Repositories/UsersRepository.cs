using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Data.Contract.Model;
using TestTask.Data.Contract.Rrepository;

namespace TestTask.Data.EF.Repositories;

internal sealed class UsersRepository(DataContext db) : IUsersRepository
{
    #region IUsersRepository implementation

    public async Task<IReadOnlyList<DataUser>> GetAllAsync()
    {
        return await db.Users.OrderBy(u => u.Login).ToListAsync();
    }

    public async Task AddAsync(DataUser user)
    {
        db.Users.Add(user);

        await db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(DataUser user)
    {
        var updatedCount = await db.Users
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(u => u.Login, user.Login)
                .SetProperty(u => u.PasswordHash, user.PasswordHash));

        return updatedCount > 0;
    }

    public async Task<bool> DeleteAsync(DataUser user)
    {
        var dataUser = await db.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
        
        if (dataUser is null)
            return false;

        db.Users.Remove(dataUser);

        await db.SaveChangesAsync();

        return true;
    }

    public Task<DataUser?> FindByIdAsync(int id)
    {
        return db.Users.Where(u => u.Id == id).SingleOrDefaultAsync();
    }

    public Task<DataUser?> FindByLoginAsync(string login)
    {
        return db.Users
            .Where(u => u.Login == login)
            .SingleOrDefaultAsync();
    }

    #endregion
}
