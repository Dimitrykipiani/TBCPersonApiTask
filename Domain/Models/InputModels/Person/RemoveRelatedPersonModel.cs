﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.InputModels.Person
{
    public class RemoveRelatedPersonModel
    {
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
    }
}
