using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class RegisterInformationDTO
    {
        public Guid Id { get; set; }
        public bool? ResultForUniversity { get; set; }
        public bool StudyAtHighSchool { get; set; }
        public bool? Graduated { get; set; }
        public Guid ClusterContestId { get; set; }
        public string ClusterContestCode { get; set; }
        public string ClusterContestName { get; set; }
        public Guid RegisterPlaceOfExamId { get; set; }
        public string RegisterPlaceOfExamCode { get; set; }
        public string RegisterPlaceOfExamName { get; set; }
        public bool? Maths { get; set; }
        public bool? Literature { get; set; }
        public string Languages { get; set; }
        public bool? NaturalSciences { get; set; }
        public bool? SocialSciences { get; set; }
        public bool? Physics { get; set; }
        public bool? Chemistry { get; set; }
        public bool? Biology { get; set; }
        public bool? History { get; set; }
        public bool? Geography { get; set; }
        public bool? CivicEducation { get; set; }
    }
}
