using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class HighSchoolInit : CommonInit
    {
        public HighSchoolInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<HighSchoolDAO> ethnics = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(ethnics);
        }

        private List<HighSchoolDAO> LoadFromExcel(string path)
        {
            List<HighSchoolDAO> excelTemplates = new List<HighSchoolDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[8];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    string provinceCode = worksheet.Cells[i, 1].Value?.ToString();

                    if (string.IsNullOrEmpty(provinceCode))
                    {
                        continue;
                    }
                    string districtCode = worksheet.Cells[i, 2].Value?.ToString();
                    if (districtCode.Equals("00"))
                    {
                        continue;
                    }
                    string highSchoolCode = worksheet.Cells[i, 3].Value?.ToString();
                    string highSchoolName = worksheet.Cells[i, 4].Value?.ToString();
                    string address = worksheet.Cells[i, 5].Value?.ToString();
                    //string areaCode = worksheet.Cells[i, 6].Value?.ToString();

                    if (provinceCode.Length < 2)
                    {
                        provinceCode = "0" + provinceCode;
                    }
                    if (districtCode.Length < 2)
                    {
                        districtCode = "0" + districtCode;
                    }
                    if (highSchoolCode.Length < 3)
                    {
                        if (highSchoolCode.Length < 2)
                            highSchoolCode = "00" + highSchoolCode;
                        else highSchoolCode = "0" + highSchoolCode;
                    }

                    HighSchoolDAO excelTemplate = new HighSchoolDAO()
                    {
                        Id = CreateGuid("HighSchool" + provinceCode + districtCode + highSchoolCode),
                        DistrictId = CreateGuid("District" + provinceCode + districtCode),
                        //AreaId = CreateGuid("Area" + areaCode),
                        Code = highSchoolCode,
                        Name = highSchoolName,
                        Address = address
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }
    }
}
