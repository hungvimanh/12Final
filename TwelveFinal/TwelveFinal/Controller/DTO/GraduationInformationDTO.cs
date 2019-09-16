using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class GraduationInformationDTO : DataDTO
    {
        public Guid Id { get; set; }
        public string ExceptLanguages { get; set; }
        public int? Mark { get; set; }
        public int? ReserveMaths { get; set; }
        public int? ReservePhysics { get; set; }
        public int? ReserveChemistry { get; set; }
        public int? ReserveLiterature { get; set; }
        public int? ReserveHistory { get; set; }
        public int? ReserveGeography { get; set; }
        public int? ReserveBiology { get; set; }
        public int? ReserveCivicEducation { get; set; }
        public int? ReserveLanguages { get; set; }
    }
}
