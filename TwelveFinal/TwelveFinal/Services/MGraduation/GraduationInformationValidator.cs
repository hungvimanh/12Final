using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MGraduation
{
    public interface IGraduationInformationValidator : IServiceScoped
    {
        Task<bool> Check(GraduationInformation graduationInformation);
    }
    public class GraduationInformationValidator : IGraduationInformationValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            Invalid
        }

        public GraduationInformationValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Check(GraduationInformation graduationInformation)
        {
            bool IsValid = true;
            IsValid &= await InputValidate(graduationInformation);
            return IsValid;
        }

        private async Task<bool> InputValidate(GraduationInformation graduationInformation)
        {
            //Kiểm tra miễn thi ngoại ngữ
            //Nếu chọn miễn thi ngoại ngữ
            //Cần ghi tên chứng chỉ và số điểm
            if(!string.IsNullOrEmpty(graduationInformation.ExceptLanguages) && graduationInformation.ExceptLanguages.Length > 500)
            {
                graduationInformation.AddError(nameof(GraduationInformationValidator), nameof(graduationInformation.ExceptLanguages), ErrorCode.Invalid);
            }

            if(!string.IsNullOrEmpty(graduationInformation.ExceptLanguages) && graduationInformation.Mark == null)
            {
                graduationInformation.AddError(nameof(GraduationInformationValidator), nameof(graduationInformation.Mark), ErrorCode.Invalid);
            }

            //Kiểm tra số điểm các môn bảo lưu nếu có 
            if(graduationInformation.ReserveMaths != null && !(graduationInformation.ReserveMaths >= 0 && graduationInformation.ReserveMaths <= 10))
            {
                graduationInformation.AddError(nameof(GraduationInformationValidator), nameof(graduationInformation.ReserveMaths), ErrorCode.Invalid);
            }

            if (graduationInformation.ReservePhysics != null && !(graduationInformation.ReservePhysics >= 0 && graduationInformation.ReservePhysics <= 10))
            {
                graduationInformation.AddError(nameof(GraduationInformationValidator), nameof(graduationInformation.ReservePhysics), ErrorCode.Invalid);
            }

            if (graduationInformation.ReserveChemistry != null && !(graduationInformation.ReserveChemistry >= 0 && graduationInformation.ReserveChemistry <= 10))
            {
                graduationInformation.AddError(nameof(GraduationInformationValidator), nameof(graduationInformation.ReserveChemistry), ErrorCode.Invalid);
            }

            if (graduationInformation.ReserveLiterature != null && !(graduationInformation.ReserveLiterature >= 0 && graduationInformation.ReserveLiterature <= 10))
            {
                graduationInformation.AddError(nameof(GraduationInformationValidator), nameof(graduationInformation.ReserveLiterature), ErrorCode.Invalid);
            }

            if (graduationInformation.ReserveHistory != null && !(graduationInformation.ReserveHistory >= 0 && graduationInformation.ReserveHistory <= 10))
            {
                graduationInformation.AddError(nameof(GraduationInformationValidator), nameof(graduationInformation.ReserveHistory), ErrorCode.Invalid);
            }

            if (graduationInformation.ReserveGeography != null && !(graduationInformation.ReserveGeography >= 0 && graduationInformation.ReserveGeography <= 10))
            {
                graduationInformation.AddError(nameof(GraduationInformationValidator), nameof(graduationInformation.ReserveGeography), ErrorCode.Invalid);
            }

            if (graduationInformation.ReserveBiology != null && !(graduationInformation.ReserveBiology >= 0 && graduationInformation.ReserveBiology <= 10))
            {
                graduationInformation.AddError(nameof(GraduationInformationValidator), nameof(graduationInformation.ReserveBiology), ErrorCode.Invalid);
            }

            if (graduationInformation.ReserveCivicEducation != null && !(graduationInformation.ReserveCivicEducation >= 0 && graduationInformation.ReserveCivicEducation <= 10))
            {
                graduationInformation.AddError(nameof(GraduationInformationValidator), nameof(graduationInformation.ReserveCivicEducation), ErrorCode.Invalid);
            }

            if (graduationInformation.ReserveLanguages != null && !(graduationInformation.ReserveLanguages >= 0 && graduationInformation.ReserveLanguages <= 10))
            {
                graduationInformation.AddError(nameof(GraduationInformationValidator), nameof(graduationInformation.ReserveLanguages), ErrorCode.Invalid);
            }

            return graduationInformation.IsValidated;
        }
    }
}
