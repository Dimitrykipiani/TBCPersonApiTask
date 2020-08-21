using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.PersonAggregate
{
    public class RelatedPerson
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int RelatedPersonId { get; set; }

        public PersonRelationType RelationType { get; set; }
    }
}
