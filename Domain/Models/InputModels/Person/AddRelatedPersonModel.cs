using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.InputModels.Person
{
    public class AddRelatedPersonModel
    {
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
        public PersonRelationType RelationType { get; set; }
    }
}
