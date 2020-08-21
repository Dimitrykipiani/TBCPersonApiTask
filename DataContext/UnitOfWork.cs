using DataContext.Contexts;
using DataContext.Repositories.Abstraction;
using Domain.Models.PersonAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public class UnitOfWork
    {
        private readonly PersonReferenceDbContext _context;

        public readonly IPersonRepository _personRepository;

        public UnitOfWork(PersonReferenceDbContext context, IPersonRepository personRepository)
        {
            _context = context;
            _personRepository = personRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
