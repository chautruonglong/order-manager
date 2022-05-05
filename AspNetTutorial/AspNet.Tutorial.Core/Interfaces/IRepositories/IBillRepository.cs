using System;
using System.Linq;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.Entities;

namespace AspNet.Tutorial.Core.Interfaces.IRepositories
{
    public interface IBillRepository : ICrudRepository<Bill, Guid>
    {
    }
}
