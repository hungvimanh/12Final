using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class EthnicInit : CommonInit
    {
        public EthnicInit(TFContext _context) : base(_context)
        {

        }

        public Task<List<Ethnic>> LoadFromExcel(byte[] file)
        {
            List<Ethnic> excelTemplates = new List<Ethnic>();
            using (MemoryStream ms = new MemoryStream(file))
            using (var package = new ExcelPackage(ms))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    Ethnic excelTemplate = new Ethnic()
                    {
                        //Code = worksheet.Cells[i, 2].Value?.ToString(),
                        //Name = worksheet.Cells[i, 3].Value?.ToString(),
                        //Description = worksheet.Cells[i, 4].Value?.ToString(),
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return Task.FromResult(excelTemplates);
        }
    }
}
