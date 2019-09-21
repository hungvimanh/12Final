using System;
using System.Collections.Generic;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class PriorityTypeInit : CommonInit
    {
        public PriorityTypeInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            DbContext.PriorityType.Add(new PriorityTypeDAO
            {
                Id = CreateGuid("01"),
                Code = "01",
            });
            DbContext.PriorityType.Add(new PriorityTypeDAO
            {
                Id = CreateGuid("02"),
                Code = "02",
            });
            DbContext.PriorityType.Add(new PriorityTypeDAO
            {
                Id = CreateGuid("03"),
                Code = "03",
            });
            DbContext.PriorityType.Add(new PriorityTypeDAO
            {
                Id = CreateGuid("04"),
                Code = "04",
            });
            DbContext.PriorityType.Add(new PriorityTypeDAO
            {
                Id = CreateGuid("05"),
                Code = "05",
            });
            DbContext.PriorityType.Add(new PriorityTypeDAO
            {
                Id = CreateGuid("06"),
                Code = "06",
            });
            DbContext.PriorityType.Add(new PriorityTypeDAO
            {
                Id = CreateGuid("07"),
                Code = "07",
            });
        }
    }
}
