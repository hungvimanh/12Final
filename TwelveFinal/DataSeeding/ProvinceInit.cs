using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class ProvinceInit : CommonInit
    {
        public ProvinceInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<ProvinceDAO> ethnics = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(ethnics);
        }
        private List<ProvinceDAO> LoadFromExcel(string path)
        {
            List<ProvinceDAO> excelTemplates = new List<ProvinceDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[5];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    ProvinceDAO excelTemplate = new ProvinceDAO()
                    {
                        Id = CreateGuid("Province" + worksheet.Cells[i, 1].Value?.ToString()),
                        Code = worksheet.Cells[i, 1].Value?.ToString(),
                        Name = worksheet.Cells[i, 2].Value?.ToString(),
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }
    }
}
