using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BoF.Web.Models
{
    public class PatientModel
    {
        public int Id { get; set; }
        public string NHN { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Sex { get; set; }
        public string Ethnicity { get; set; }
        public string Division { get; set; }
        public bool Deceased { get; set; }
        public string CurrentAddress { get; set; }
        public string AddressDiagnosis { get; set; }
        public string ContactPerson1 { get; set; }
        public string Relationship1 { get; set; }
        public string PhoneContact1 { get; set; }
        public string ContactPerson2 { get; set; }
        public string Relationship2 { get; set; }
        public string PhoneContact2 { get; set; }
        public string CurrentSchool { get; set; }

    }
}