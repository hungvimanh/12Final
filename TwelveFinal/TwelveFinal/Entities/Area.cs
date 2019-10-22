using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Area : DataEntity  //Khu vực tuyển sinh
    {
        public Guid Id { get; set; }
        public string Code { get; set; }    //Mã khu vực
        public string Name { get; set; }    //Tên khu vực
    }

    public class AreaFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public AreaOrder OrderBy { get; set; }
        public AreaFilter() : base()
        {

        }
    }

    public enum AreaOrder
    {
        CX,
        Code,
        Name
    }
}
