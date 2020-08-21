using Application.Services.Abstraction;
using Domain.Models.InputModels.Person;
using Domain.Models.PersonAggregate;
using Domain.Models.ReadModels;
using Domain.Models.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using PersonsReference.Utility;
using PersonsReference.Utility.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace PersonsReference.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<IEnumerable<PersonReadModel>> GetPeople()
        {
            return await _personService.GetPeopleAsync();
        }

        [HttpGet]
        public async Task<PersonReadModel> GetPerson([FromBody] GetPersonModel model)
        {
            return await _personService.GetPersonById(model.Id);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody]Person person)
        {
            await _personService.AddPerson(person);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonModel updatePersonModel)
        {
            var updateResult = await _personService.UpdatePerson(updatePersonModel);

            if (updateResult == false)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeletePerson(DeletePersonModel model)
        {
            var deletionResult = await _personService.DeletePerson(model.Id);

            if (deletionResult == false)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, int personId)
        {
            if (file == null)
                return BadRequest();

            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString();
            var fullImageName = $"{fileName}.{extension}";
            var pathToImage = Path.Combine(PathContstants.PathToImagesDirectory, fullImageName);

            var result = await _personService.UploadImage(pathToImage, personId);

            if (result == false)
                return BadRequest();

            using (var stream = System.IO.File.Create(pathToImage))
            {
                await file.CopyToAsync(stream);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddRelatedPerson(AddRelatedPersonModel model)
        {
            var result = await _personService.AddRelatedPerson(model);

            if (result == false)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRelatedPerson(RemoveRelatedPersonModel model)
        {
            var result = await _personService.RemoveRelatedPerson(model);

            if (result == false)
                return BadRequest();

            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<PersonReadModel>> SearchPeople(SearchPeopleModel model)
        {
            return await _personService.SearchPeopleAsync(model);
        }

        [HttpGet]
        public async Task<IEnumerable<RelatedPersonInfoModel>> GetRelatedPersonInfo()
        {
            return await _personService.GetRelatedPersonInfo();
        }
    }
}
