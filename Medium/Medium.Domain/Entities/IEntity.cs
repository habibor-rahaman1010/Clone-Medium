using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.Domain.Entities
{
    public interface IEntity<T> where T : IComparable<T>
    {
        public T Id { get; set; }
    }
}
