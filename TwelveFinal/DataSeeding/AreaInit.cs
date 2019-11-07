//using OfficeOpenXml;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using TwelveFinal.Repositories.Models;

//namespace DataSeeding
//{
//    public class AreaInit : CommonInit
//    {
//        public AreaInit(TFContext _context) : base(_context)
//        {

//        }

//        public void Init()
//        {
//            List<AreaDAO> ethnics = LoadFromExcel("../../../DataSeeding.xlsx");
//            DbContext.AddRange(ethnics);
//        }
//        private List<AreaDAO> LoadFromExcel(string path)
//        {
//            List<AreaDAO> excelTemplates = new List<AreaDAO>();
//            using (var package = new ExcelPackage(new FileInfo(path)))
//            {
//                var worksheet = package.Workbook.Worksheets[0];
//                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
//                {
//                    AreaDAO excelTemplate = new AreaDAO()
//                    {
//                        Id = CreateGuid("Area" + worksheet.Cells[i, 2].Value?.ToString()),
//                        Code = worksheet.Cells[i, 1].Value?.ToString(),
//                        Name = worksheet.Cells[i, 2].Value?.ToString(),
//                    };
//                    excelTemplates.Add(excelTemplate);
//                }
//            }
//            return excelTemplates;
//        }
//    }
//}
