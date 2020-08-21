using Domain.Enums;
using Domain.Models.PersonAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Shared
{
    public class ValidationHelperMethods
    {
        public static bool ShouldContainOnlyLatinOrOnlyGeorgianCharacters(string text)
        {
            var regexEnglish = new Regex("^[a-zA-Z]+$");
            var regexGeo = new Regex("^[ა-ზ]+$");

            return regexEnglish.IsMatch(text) || regexGeo.IsMatch(text);
        }

        public static bool MustBeFemaleOrMale(Sex sex)
        {
            bool isMale = sex == Sex.Male;
            bool isFemale = sex == Sex.Female;

            return (isMale || isFemale);
        }

        public static bool PhoneNumbersAreValid(IEnumerable<PhoneNumber> phoneNumbers)
        {
            bool isValid = true;

            if (phoneNumbers != null && phoneNumbers.Any())
            {
                foreach (var phoneNumber in phoneNumbers)
                {
                    if (phoneNumber != null)
                    {
                        bool isHouseNumber = phoneNumber.PhoneType == PhoneType.Home;
                        bool isHouseOfficeNumber = phoneNumber.PhoneType == PhoneType.Office;
                        bool isHouseMobile = phoneNumber.PhoneType == PhoneType.Mobile;

                        isValid = (isHouseNumber || isHouseOfficeNumber || isHouseMobile);
                    }
                }
            }

            return isValid;
        }

        public static bool RelatedPeopleAreValid(IEnumerable<RelatedPerson> relatedPeople)
        {
            bool isValid = true;

            if (relatedPeople != null && relatedPeople.Any())
            {
                foreach (var relatedPerson in relatedPeople)
                {
                    if (relatedPeople != null)
                    {
                        bool isColleague = relatedPerson.RelationType == PersonRelationType.Colleague;
                        bool isAcquaintance = relatedPerson.RelationType == PersonRelationType.Acquaintance;
                        bool isRelative = relatedPerson.RelationType == PersonRelationType.Relative;
                        bool isOther = relatedPerson.RelationType == PersonRelationType.Other;

                        isValid = (isColleague || isAcquaintance || isRelative || isOther);
                    }
                }
            }

            return isValid;
        }
    }
}
