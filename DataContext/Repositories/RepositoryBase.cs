using DataContext.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Repositories
{
    public class RepositoryBase
    {
        protected readonly PersonReferenceDbContext _context;

        public RepositoryBase(PersonReferenceDbContext context)
        {
            _context = context;
        }
    }
}
