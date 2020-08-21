using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.ReadModels
{
    public class PhoneNumberReadModel
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public PhoneType PhoneType { get; set; }

        public string Number { get; set; }
    }
}
