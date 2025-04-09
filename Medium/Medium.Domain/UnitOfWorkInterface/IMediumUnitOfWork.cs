using Medium.Domain.RepositoriesInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.Domain.UnitOfWorkInterface
{
    public interface IMediumUnitOfWork : IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
    }
}
