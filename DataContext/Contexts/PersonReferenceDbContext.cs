using DataContext.Configurations;
using DataContext.Extensions;
using Domain.Models.PersonAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Contexts
{
    public class PersonReferenceDbContext : DbContext
    {
        public PersonReferenceDbContext(DbContextOptions<PersonReferenceDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new RelatedPersonConfiguration());
            modelBuilder.ApplyConfiguration(new PhoneNumberConfiguration());

            modelBuilder.Seed();
        }

        public DbSet<Person> People { get; set; }
        public DbSet<RelatedPerson> Relations { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    }
}
