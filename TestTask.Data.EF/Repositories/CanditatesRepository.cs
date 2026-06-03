using Microsoft.EntityFrameworkCore;
using TestTask.Data.Contract.Rrepository;
using TestTask.Data.Contract.Model;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Data.EF.Repositories;

internal sealed class CanditatesRepository(DataContext db) : ICanditatesRrepositry
{
    #region ICanditatesRrepositry implementation

    public async Task<IReadOnlyList<DataCandidate>> GetAllAsync()
    {
        return await db.Candidates.OrderByDescending(x => x.CreatedAtUtc).ToListAsync();
    }

    public async Task AddAsync(DataCandidate candidate)
    {
        db.Candidates.Add(candidate);

        await db.SaveChangesAsync();
    }

    #endregion
}
