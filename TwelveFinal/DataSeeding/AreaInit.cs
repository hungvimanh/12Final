using System;
using System.Collections.Generic;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class AreaInit : CommonInit
    {
        public AreaInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            DbContext.Area.Add(new AreaDAO
            {
                Id = CreateGuid("KV1"),
                Code = "1-KV1",
                Name = "KV1"
            });
            DbContext.Area.Add(new AreaDAO
            {
                Id = CreateGuid("KV2"),
                Code = "2-KV2",
                Name = "KV2",
            });
            DbContext.Area.Add(new AreaDAO
            {
                Id = CreateGuid("KV2-NT"),
                Code = "2NT-KV2-NT",
                Name = "KV2-NT",
            });
            DbContext.Area.Add(new AreaDAO
            {
                Id = CreateGuid("KV3"),
                Code = "KV3",
                Name = "3-KV3",
            });
        }
    }
}
