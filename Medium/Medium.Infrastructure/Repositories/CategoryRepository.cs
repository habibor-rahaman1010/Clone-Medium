using Medium.Domain.Entities;
using Medium.Domain.RepositoriesInterface;
using Medium.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        private readonly MediumDbContext _mediumDbContext;

        public CategoryRepository(MediumDbContext dbContext) : base(dbContext)
        {
            _mediumDbContext = dbContext;
        }
 
    }
}
