using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.ReadModels
{
    public class PersonReadModel
    {
        public int Id { get; set; }
        public int CityId { get; set; }

        public string FistName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        public string ImageURL { get; set; }

        public Sex Sex { get; set; }

        public DateTime BirthDate { get; set; }

        public List<PhoneNumberReadModel> PhoneNumbers { get; set; }
        public List<RelatedPersonReadModel> RelatedPeople { get; set; }
    }
}
