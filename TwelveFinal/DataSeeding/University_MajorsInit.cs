using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class University_MajorsInit : CommonInit
    {
        public University_MajorsInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<University_MajorsDAO> university_MajorsDAOs = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(university_MajorsDAOs);
        }

        private List<University_MajorsDAO> LoadFromExcel(string path)
        {
            List<University_MajorsDAO> excelTemplates = new List<University_MajorsDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[4];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    string universityCode = worksheet.Cells[i, 1].Value?.ToString();
                    string majorsCode = worksheet.Cells[i, 2].Value?.ToString();
                    string subjectGroupCode = worksheet.Cells[i, 3].Value?.ToString();

                    University_MajorsDAO excelTemplate = new University_MajorsDAO()
                    {
                        UniversityId = CreateGuid("University" + universityCode),
                        MajorsId = CreateGuid("Majors" + majorsCode),
                        SubjectGroupId = CreateGuid("SubjectGroup" + subjectGroupCode),
                        Year = worksheet.Cells[i, 4].Value?.ToString(),
                        Benchmark = Convert.ToDouble(worksheet.Cells[i, 5].Value?.ToString()),
                        Descreption = worksheet.Cells[i, 6].Value?.ToString(),
                        Quantity = Convert.ToInt32(worksheet.Cells[i, 7].Value?.ToString())
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }
    }
}
