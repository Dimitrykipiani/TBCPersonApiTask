using Application.Services.Abstraction;
using DataContext;
using Domain.Models.InputModels.Person;
using Domain.Models.PersonAggregate;
using Domain.Models.ReadModels;
using Domain.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly UnitOfWork _unitOfWork;

        public PersonService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddPerson(Person person)
        {
            var result = Task.FromResult(_unitOfWork._personRepository.AddPerson(person));

            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> AddRelatedPerson(AddRelatedPersonModel model)
        {
            var result = await _unitOfWork._personRepository.AddRelatedPerson(model);

            if (result == false)
                return false;

            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeletePerson(int id)
        {
            var result = await _unitOfWork._personRepository.DeletePerson(id);

            if (result == false)
                return false;

            await _unitOfWork.SaveAsync();
            return result;
        }

        public async Task<IEnumerable<PersonReadModel>> GetPeopleAsync()
        {
            var people = await _unitOfWork._personRepository.GetPeopleAsync();

            return people;
        }

        public async Task<PersonReadModel> GetPersonById(int id)
        {
            var person = await _unitOfWork._personRepository.GetPersonById(id);

            return person;
        }

        public async Task<IEnumerable<RelatedPersonInfoModel>> GetRelatedPersonInfo()
        {
            return await _unitOfWork._personRepository.GetRelatedPersonInfo();
        }

        public async Task<bool> RemoveRelatedPerson(RemoveRelatedPersonModel model)
        {
            var result = await _unitOfWork._personRepository.RemoveRelatedPerson(model);

            if (result == false)
                return false;

            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<PersonReadModel>> SearchPeopleAsync(SearchPeopleModel model)
        {
            return await _unitOfWork._personRepository.SearchPeopleAsync(model);
        }

        public async Task<bool> UpdatePerson(UpdatePersonModel updatePersonModel)
        {
            var result = await _unitOfWork._personRepository.UpdatePerson(updatePersonModel);

            await _unitOfWork.SaveAsync();

            return result;
        }

        public async Task<bool> UploadImage(string url, int id)
        {
            var result = await _unitOfWork._personRepository.UploadImage(url, id);

            if (result == false)
                return false;

            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
