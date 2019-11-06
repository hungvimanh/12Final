using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Form : DataEntity
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public Guid UserId { get; set; }

        public string FullName { get; set; }    //họ và tên đầy đủ
        public DateTime? Dob { get; set; }   //Ngày tháng năm sinh
        public bool? Gender { get; set; }    //Giới tính
        public string PlaceOfBirth { get; set; }    //Nơi sinh
        public string Ethnic { get; set; }      //Dân tộc
        public string Identify { get; set; }    //số CMT/Căn cước công dân
        public Guid TownId { get; set; }    //Hộ khẩu thường trú
        public string TownCode { get; set; }
        public string TownName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public bool? IsPermanentResidenceMore18 { get; set; }   //Hộ khẩu thường trú KV1 > 18tháng
        public bool? IsPermanentResidenceSpecialMore18 { get; set; }    //Hộ khẩu thường trú vùng đặc biệt khó khăn >18 tháng
        public Guid HighSchoolGrade10Id { get; set; }   //Trường THPT
        public string HighSchoolGrade10Code { get; set; }
        public string HighSchoolGrade10Name { get; set; }
        public Guid HighSchoolGrade11Id { get; set; }
        public string HighSchoolGrade11Code { get; set; }
        public string HighSchoolGrade11Name { get; set; }
        public Guid HighSchoolGrade12Id { get; set; }
        public string HighSchoolGrade12Code { get; set; }
        public string HighSchoolGrade12Name { get; set; }
        public string Phone { get; set; }   //Sđt
        public string Email { get; set; }   
        public string Address { get; set; }     //Địa chỉ liên hệ

        public bool? ResultForUniversity { get; set; }      //Thí sinh sử dụng kết quả thi để xét tuyển CĐ-ĐH?
        public bool StudyAtHighSchool { get; set; }     //Thí sinh học THPT(True) - TTGDTX(False)
        public bool? Graduated { get; set; }        //Đã tốt nghiệp THPT/TTGDTX?
        public Guid ClusterContestId { get; set; }      //Cụm thi(tỉnh/thành phố)
        public string ClusterContestCode { get; set; }
        public string ClusterContestName { get; set; }
        public Guid RegisterPlaceOfExamId { get; set; }     //Địa điểm đăng kí thi(trường THPT)
        public string RegisterPlaceOfExamCode { get; set; }
        public string RegisterPlaceOfExamName { get; set; }

        //Các môn dự thi
        public bool? Maths { get; set; }
        public bool? Literature { get; set; }
        public string Languages { get; set; }
        public bool? NaturalSciences { get; set; }      //Tổ hợp KHTN
        public bool? SocialSciences { get; set; }       //Tổ hợp KHXH
        public bool? Physics { get; set; }
        public bool? Chemistry { get; set; }
        public bool? Biology { get; set; }
        public bool? History { get; set; }
        public bool? Geography { get; set; }
        public bool? CivicEducation { get; set; }

        //Phần điểm miễn thi ngoại ngữ (nếu có)
        public string ExceptLanguages { get; set; }
        //Các môn học muốn bảo lưu điểm
        public int? Mark { get; set; }
        public int? ReserveMaths { get; set; }
        public int? ReservePhysics { get; set; }
        public int? ReserveChemistry { get; set; }
        public int? ReserveLiterature { get; set; }
        public int? ReserveHistory { get; set; }
        public int? ReserveGeography { get; set; }
        public int? ReserveBiology { get; set; }
        public int? ReserveCivicEducation { get; set; }
        public int? ReserveLanguages { get; set; }

        public string PriorityType { get; set; }        //Đối tượng ưu tiên
        public string Area { get; set; }        //Khu vực tuyển sinh

        public List<Aspiration> Aspirations { get; set; }       //Danh sách các nguyện vọng thi sinh đăng kí
    }

   
}
