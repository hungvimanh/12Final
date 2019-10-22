﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class ProvinceDTO : DataDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class ProvinceFilterDTO : FilterDTO
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public ProvinceFilterDTO() : base()
        {

        }
    }
}
