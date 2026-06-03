using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Data.Contract.Bases;

namespace TestTask.Data.Contract.Abstration;

public interface IRepository<T> where T : DataModelBase
{
    Task AddAsync(T model);
    Task<IReadOnlyList<T>> GetAllAsync();
}
