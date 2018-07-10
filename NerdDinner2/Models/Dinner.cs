using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace NerdDinner2.Models
{
    [Bind(Include = "Title,Description,EventDate,Address,Country,ContactPhone,Latitude,Longitude")]
    public class Dinner
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int DinnerID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string Description { get; set; }

        //[Required]
        public string HostedBy { get; set; }

        [Required]
        public string ContactPhone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public virtual ICollection<RSVP> RSVPs { get; set; }

        public bool IsUserRegistered(string userName)
        {
            return RSVPs.Any(r => r.AttendeeEmail.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool IsValid
        {
            //get { return (GetRuleViolations().Count() == 0); }
            return (Title == )
        }

        public bool IsHostedBy(string userName)
        {
            return HostedBy.Equals(userName, StringComparison.InvariantCultureIgnoreCase);
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {

            if (String.IsNullOrEmpty(Title))
                yield return new RuleViolation("Title required", "Title");

            if (String.IsNullOrEmpty(Description))
                yield return new RuleViolation("Description required", "Description");

            if (String.IsNullOrEmpty(HostedBy))
                yield return new RuleViolation("HostedBy required", "HostedBy");

            if (String.IsNullOrEmpty(Address))
                yield return new RuleViolation("Address required", "Address");

            if (String.IsNullOrEmpty(Country))
                yield return new RuleViolation("Country required", "Country");

            if (String.IsNullOrEmpty(ContactPhone))
                yield return new RuleViolation("Phone# required", "ContactPhone");

            if (!PhoneValidator.IsValidNumber(ContactPhone, Country))
                yield return new RuleViolation("Phone# does not match country", "ContactPhone");


            yield break;
        }
    }

    public class RuleViolation
    {
        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }
        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }

    public class PhoneValidator
    {

        public static IEnumerable<string> AllCountries = new string[]{"USA", "UK", "Netherlands"};

        static IDictionary<string, Regex> countryRegex = new Dictionary<string, Regex>() {
            { "USA", new Regex("^[2-9]\\d{2}-\\d{3}-\\d{4}$")},
            { "UK", new Regex("(^1300\\d{6}$)|(^1800|1900|1902\\d{6}$)|(^0[2|3|7|8]{1}[0-9]{8}$)|(^13\\d{4}$)|(^04\\d{2,3}\\d{6}$)")},
            { "Netherlands", new Regex("(^\\+[0-9]{2}|^\\+[0-9]{2}\\(0\\)|^\\(\\+[0-9]{2}\\)\\(0\\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\\-\\s]{10}$)")},
        };

        public static bool IsValidNumber(string phoneNumber, string country)
        {

            if (country != null && countryRegex.ContainsKey(country))
                return countryRegex[country].IsMatch(phoneNumber);
            else
                return false;
        }

        public static IEnumerable<string> Countries
        {
            get
            {
                return countryRegex.Keys;
            }
        }

    }
}