using Domain.Models.InputModels.Person;
using Domain.Models.PersonAggregate;
using Domain.Models.ReadModels;
using Domain.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories.Abstraction
{
    public interface IPersonRepository
    {
        Task<IEnumerable<PersonReadModel>> GetPeopleAsync();
        Task<IEnumerable<PersonReadModel>> SearchPeopleAsync(SearchPeopleModel model);
        Task<PersonReadModel> GetPersonById(int id);
        Task AddPerson(Person person);
        Task<bool> UpdatePerson(UpdatePersonModel model);
        Task<bool> DeletePerson(int id);
        Task<bool> UploadImage(string url, int id);
        Task<bool> AddRelatedPerson(AddRelatedPersonModel model);
        Task<bool> RemoveRelatedPerson(RemoveRelatedPersonModel model);
        Task<IEnumerable<RelatedPersonInfoModel>> GetRelatedPersonInfo();
    }
}
