using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;

namespace TwelveFinal.Controller.DTO
{
    public class UniversityAdmissionDTO
    {
        public Guid Id { get; set; }
        public string PriorityType { get; set; }
        public string Area { get; set; }
        public string GraduateYear { get; set; }
        public bool? Connected { get; set; }
        public int TotalAspiration { get; set; }
        public List<FormDetailDTO> FormDetailDTOs { get; set; }
    }
}
