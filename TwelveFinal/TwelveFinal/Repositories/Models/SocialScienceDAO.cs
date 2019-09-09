using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class SocialScienceDAO
    {
        public SocialScienceDAO()
        {
            TestExams = new HashSet<TestExamDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool History { get; set; }
        public bool Geography { get; set; }
        public bool CivicEducation { get; set; }

        public virtual ICollection<TestExamDAO> TestExams { get; set; }
    }
}
