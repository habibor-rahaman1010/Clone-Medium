using Medium.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.Domain.RepositoriesInterface
{
    public interface ICategoryRepository : IRepository<Category,  Guid>
    {
    }
}
