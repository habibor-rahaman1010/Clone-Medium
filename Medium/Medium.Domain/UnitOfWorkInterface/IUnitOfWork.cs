using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.Domain.UnitOfWorkInterface
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        public void Save();
        public Task SaveAsync();
    }
}
