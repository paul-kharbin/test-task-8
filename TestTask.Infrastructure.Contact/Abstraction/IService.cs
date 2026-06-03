using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Infrastructure.Contact.Bases;

namespace TestTask.Infrastructure.Contact.Abstraction;

public interface IService<T> where T : ModelBase
{
    Task<IReadOnlyList<T>> GetAllAsync();
}
