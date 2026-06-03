using System.Threading.Tasks;
using TestTask.Infrastructure.Contact.Abstraction;
using TestTask.Infrastructure.Contact.Model;

namespace TestTask.Infrastructure.Contact.Services;

public interface ICanditatesService : IService<Candidate>
{
    Task<Candidate> AddAsync(Candidate candidate);
}
