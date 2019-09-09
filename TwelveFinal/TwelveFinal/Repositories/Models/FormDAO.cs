using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class FormDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public string NumberForm { get; set; }
        public string DepartmentCode { get; set; }
        public DateTime Date { get; set; }
        public Guid PersonalInfomartionId { get; set; }
        public Guid RegisterInformationId { get; set; }
        public Guid GraduationInformationId { get; set; }
        public Guid UniversityAdmissionId { get; set; }

        public virtual GraduationInformationDAO GraduationInformation { get; set; }
        public virtual PersonalInformationDAO PersonalInfomartion { get; set; }
        public virtual RegisterInformationDAO RegisterInformation { get; set; }
        public virtual UniversityAdmissionDAO UniversityAdmission { get; set; }
    }
}
