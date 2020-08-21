using Domain.Enums;
using Domain.Models.PersonAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person()
                {
                    Id = 1,
                    CityId = 2,
                    FistName = "George",
                    LastName = "Washington",
                    PrivateNumber = "04001099344",
                    ImageURL = "",
                    Sex = Sex.Male,
                    BirthDate = new DateTime(1732, 2, 22)
                },
                new Person()
                {
                    Id = 2,
                    CityId = 2,
                    FistName = "Abraham",
                    LastName = "Lincoln",
                    PrivateNumber = "04041099344",
                    ImageURL = "",
                    Sex = Sex.Male,
                    BirthDate = new DateTime(1865, 2, 12)
                }
                );

            modelBuilder.Entity<RelatedPerson>().HasData(
                    new RelatedPerson() { Id = 1, PersonId = 1, RelatedPersonId = 2, RelationType = PersonRelationType.Colleague },
                    new RelatedPerson() { Id = 2, PersonId = 2, RelatedPersonId = 1, RelationType = PersonRelationType.Colleague }

                );

            modelBuilder.Entity<PhoneNumber>().HasData(
                new PhoneNumber()
                {
                    Id = 1,
                    Number = "555111222",
                    PhoneType = PhoneType.Home,
                    OwnerId = 1
                },
                new PhoneNumber()
                {
                    Id = 2,
                    Number = "555111223",
                    PhoneType = PhoneType.Office,
                    OwnerId = 1
                },
                new PhoneNumber()
                {
                    Id = 3,
                    Number = "555111224",
                    PhoneType = PhoneType.Mobile,
                    OwnerId = 1
                }
                );
        }
    }
}
