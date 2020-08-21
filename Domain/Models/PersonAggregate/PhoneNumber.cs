using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.PersonAggregate
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }
        public Person Owner { get; set; }

        public PhoneType PhoneType { get; set; }

        public string Number { get; set; }
    }
}
