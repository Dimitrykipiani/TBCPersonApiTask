using DataContext.Contexts;
using DataContext.Repositories.Abstraction;
using Domain.Models.InputModels.Person;
using Domain.Models.PersonAggregate;
using Domain.Models.ReadModels;
using Domain.Models.RequestModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories.Implementation
{
    public class PersonRepository : RepositoryBase, IPersonRepository
    {
        public PersonRepository(PersonReferenceDbContext context) : base(context)
        {

        }

        public Task AddPerson(Person person)
        {
            return Task.FromResult(_context.Set<Person>().AddAsync(person));
        }

        public async Task<bool> AddRelatedPerson(AddRelatedPersonModel model)
        {
            var targetPerson = await _context.Set<Person>()
                .FirstOrDefaultAsync(x => x.Id == model.PersonId);

            var relatedPersonToAdd = await _context.Set<Person>()
                .FirstOrDefaultAsync(x => x.Id == model.RelatedPersonId);

            if (targetPerson == null || relatedPersonToAdd == null)
                return false;

            var relatedPerson = new RelatedPerson()
            {
                Person = targetPerson,
                RelatedPersonId = model.RelatedPersonId,
                RelationType = model.RelationType
            };

            _context.Set<RelatedPerson>().Add(relatedPerson);

            targetPerson.RelatedPeople.Add(relatedPerson);

            _context.Set<Person>().Update(targetPerson);

            return true;
        }

        public async Task<bool> DeletePerson(int id)
        {
            var personToDelete = await _context.People.FirstOrDefaultAsync(x => x.Id == id);

            if (personToDelete == null)
                return false;

            _context.People.Remove(personToDelete);

            return true;
        }

        public async Task<IEnumerable<PersonReadModel>> GetPeopleAsync()
        {
            var people = await _context.People
                .Include(x => x.RelatedPeople)
                .Include(x => x.PhoneNumbers)
                .ToListAsync();

            var returnModel = people.Select(x => new PersonReadModel()
            {
                Id = x.Id,
                FistName = x.FistName,
                LastName = x.LastName,
                Sex = x.Sex,
                BirthDate = x.BirthDate,
                CityId = x.CityId,
                ImageURL = x.ImageURL,
                PhoneNumbers = x.PhoneNumbers.Select(pn => new PhoneNumberReadModel()
                {
                    Id = pn.Id,
                    PhoneType = pn.PhoneType,
                    Number = pn.Number,
                    OwnerId = pn.OwnerId
                }).ToList(),
                RelatedPeople = x.RelatedPeople.Select(rp => new RelatedPersonReadModel()
                {
                    Id = rp.Id,
                    PersonId = rp.PersonId,
                    RelatedPersonId = rp.RelatedPersonId,
                    RelationType = rp.RelationType
                }).ToList()
            });

            return returnModel;
        }

        public async Task<PersonReadModel> GetPersonById(int id)
        {
            var person = await _context.People
                .Include(x => x.RelatedPeople)
                .Include(x => x.PhoneNumbers)
                .FirstOrDefaultAsync(x => x.Id == id);

            var returnModel = new PersonReadModel()
            {
                Id = person.Id,
                FistName = person.FistName,
                LastName = person.LastName,
                BirthDate = person.BirthDate,
                Sex = person.Sex,
                CityId = person.CityId,
                ImageURL = person.ImageURL,
                PrivateNumber = person.PrivateNumber,
                PhoneNumbers = person.PhoneNumbers.Select(x => new PhoneNumberReadModel()
                {
                    Id = x.Id,
                    OwnerId = x.OwnerId,
                    Number = x.Number,
                    PhoneType = x.PhoneType
                }).ToList(),
                RelatedPeople = person.RelatedPeople.Select(x => new RelatedPersonReadModel()
                {
                    Id = x.Id,
                    PersonId = x.PersonId,
                    RelatedPersonId = x.RelatedPersonId,
                    RelationType = x.RelationType
                }).ToList()
            };

            return returnModel;
        }

        public async Task<IEnumerable<RelatedPersonInfoModel>> GetRelatedPersonInfo()
        {
            var persons = await _context.Set<Person>()
                .Include(x => x.RelatedPeople)
                .Select(x => new { Id = x.Id, RelatedPersons = x.RelatedPeople })
                .ToListAsync();

            var returnModel = persons.Select(x => new RelatedPersonInfoModel()
            {
                PersonId = x.Id,
                Relations = new Dictionary<Domain.Enums.PersonRelationType, int>()
            });

            foreach (var relatedPersonInfo in returnModel)
            {
                var relations = persons.First(x => x.Id == relatedPersonInfo.PersonId).RelatedPersons;

                foreach (var item in relations)
                {
                    if (relatedPersonInfo.Relations.ContainsKey(item.RelationType))

                        relatedPersonInfo.Relations[item.RelationType]++;
                    else
                    {
                        relatedPersonInfo.Relations.Add(item.RelationType, 1);
                    }
                }
            }

            return returnModel;
        }

        public async Task<bool> RemoveRelatedPerson(RemoveRelatedPersonModel model)
        {
            var targetPerson = await _context.Set<Person>()
                .Include(x => x.RelatedPeople)
                .FirstOrDefaultAsync(x => x.Id == model.PersonId);

            var relatedPersonToRemove = targetPerson.RelatedPeople
                .FirstOrDefault(x => x.RelatedPersonId == model.RelatedPersonId);

            if (targetPerson == null || relatedPersonToRemove == null)
                return false;

            targetPerson.RelatedPeople.Remove(relatedPersonToRemove);
            return true;
        }

        public async Task<IEnumerable<PersonReadModel>> SearchPeopleAsync(SearchPeopleModel model)
        {
            var people = await _context.People
               .Include(x => x.RelatedPeople)
               .Include(x => x.PhoneNumbers)
               .Where(x => x.FistName.Contains(model.SearchString, StringComparison.OrdinalIgnoreCase)
               || x.LastName.Contains(model.SearchString, StringComparison.OrdinalIgnoreCase)
               || x.PrivateNumber.Contains(model.SearchString, StringComparison.OrdinalIgnoreCase))
               .ToListAsync();

            var returnModel = people.Select(x => new PersonReadModel()
            {
                Id = x.Id,
                FistName = x.FistName,
                LastName = x.LastName,
                Sex = x.Sex,
                BirthDate = x.BirthDate,
                CityId = x.CityId,
                ImageURL = x.ImageURL,
                PhoneNumbers = x.PhoneNumbers.Select(pn => new PhoneNumberReadModel()
                {
                    Id = pn.Id,
                    PhoneType = pn.PhoneType,
                    Number = pn.Number,
                    OwnerId = pn.OwnerId
                }).ToList(),
                RelatedPeople = x.RelatedPeople.Select(rp => new RelatedPersonReadModel()
                {
                    Id = rp.Id,
                    PersonId = rp.PersonId,
                    RelatedPersonId = rp.RelatedPersonId,
                    RelationType = rp.RelationType
                }).ToList()
            });

            return returnModel;
        }

        public async Task<bool> UpdatePerson(UpdatePersonModel model)
        {
            var personToUpdate = await _context.Set<Person>()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (personToUpdate == null)
                return false;

            personToUpdate.FistName = model.FistName;
            personToUpdate.LastName = model.LastName;
            personToUpdate.Sex = model.Sex;
            personToUpdate.PrivateNumber = model.PrivateNumber;
            personToUpdate.CityId = model.CityId;
            personToUpdate.BirthDate = model.BirthDate;
            personToUpdate.PhoneNumbers = model.PhoneNumbers;

            _context.Update(personToUpdate);

            return true;
        }

        public async Task<bool> UploadImage(string url, int id)
        {
            var targetPerson = await _context.Set<Person>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (targetPerson == null)
                return false;

            targetPerson.ImageURL = url;
            _context.Update(targetPerson);

            return true;
        }
    }
}
