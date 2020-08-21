using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.PersonAggregate
{
    public class Person
    {
        public int Id { get; set; }
        public int CityId { get; set; }

        public string FistName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        public string ImageURL { get; set; }

        public Sex Sex { get; set; }

        public DateTime BirthDate { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public List<RelatedPerson> RelatedPeople { get; set; } = new List<RelatedPerson>();
    }
}
