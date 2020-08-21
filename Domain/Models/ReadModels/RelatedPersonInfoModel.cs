using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.ReadModels
{
    public class RelatedPersonInfoModel
    {
        public int PersonId { get; set; }
        public Dictionary<PersonRelationType, int> Relations { get; set; } = new Dictionary<PersonRelationType, int>();
    }
}
