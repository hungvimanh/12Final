//using OfficeOpenXml;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using TwelveFinal.Repositories.Models;

//namespace DataSeeding
//{
//    public class PriorityTypeInit : CommonInit
//    {
//        public PriorityTypeInit(TFContext _context) : base(_context)
//        {

//        }

//        public void Init()
//        {
//            List<PriorityTypeDAO> ethnics = LoadFromExcel("../../../DataSeeding.xlsx");
//            DbContext.AddRange(ethnics);
//        }
//        private List<PriorityTypeDAO> LoadFromExcel(string path)
//        {
//            List<PriorityTypeDAO> excelTemplates = new List<PriorityTypeDAO>();
//            using (var package = new ExcelPackage(new FileInfo(path)))
//            {
//                var worksheet = package.Workbook.Worksheets[1];
//                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
//                {
//                    PriorityTypeDAO excelTemplate = new PriorityTypeDAO()
//                    {
//                        Id = CreateGuid("PriorityType" + worksheet.Cells[i, 1].Value?.ToString()),
//                        Code = worksheet.Cells[i, 1].Value?.ToString(),
//                    };
//                    excelTemplates.Add(excelTemplate);
//                }
//            }
//            return excelTemplates;
//        }
//    }
//}
