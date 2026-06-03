using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestTask.Data.Contract.Rrepository;
using TestTask.Infrastructure.Contact.Model;
using TestTask.Infrastructure.Contact.Services;
using DataModel = TestTask.Data.Contract.Model;

namespace TestTask.Infrastructure.Services;

internal sealed class CanditatesService(ICanditatesRrepositry repository, IMapper mapper) : ICanditatesService
{
    #region ICanditatesService implementaition

    public async Task<IReadOnlyList<Candidate>> GetAllAsync()
    {
        var candidates = await repository.GetAllAsync();

        return candidates.Select(mapper.Map<Candidate>).ToList();
    }

    public async Task<Candidate> AddAsync(Candidate candidate)
    {
        var dataModel = mapper.Map<DataModel.DataCandidate>(candidate);

        await repository.AddAsync(dataModel);

        return mapper.Map<Candidate>(dataModel);
    }

    #endregion
}
