using Domain.Enums;
using Domain.Models.PersonAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.InputModels.Person
{
    public class UpdatePersonModel
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public string FistName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }

        public Sex Sex { get; set; }

        public DateTime BirthDate { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }
    }
}
